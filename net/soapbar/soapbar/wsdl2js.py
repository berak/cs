#!/usr/bin/python

# wsdl2js.py
#
# Creates JavaScript types from a .wsdl file
#
# Version 0.9
#
# Michael Bolin
# 2 January 2006
# http://www.bolinfest.com/wsdl2js/

import getopt, sys, string
from xml.dom.minidom import parse

# these are the JSObjects that are extracted from the <message> tags
global jsobjects
jsobjects = {}

# Returns int, double, string, boolean, base64Binary, or 0
def is_simple(obj_name):
    obj_name = remove_prefix(obj_name)
    builtinTest = is_builtin(obj_name)
    if builtinTest: return builtinTest
    if (obj_name in jsobjects.keys() and jsobjects[obj_name].num_properties() == 1):
      return is_simple((jsobjects[obj_name].propertyValues)[0])
    else:
      return 0

_BUILTINS = { "int" : 0, "double" : 0, "string" : 0, "boolean" : 0, "base64Binary" : 0 }

# True if type is int, double, string, boolean, or base64Binary
def is_builtin(type):
  type = remove_prefix(type)
  if type in _BUILTINS:
    return type
  else:
    return 0

def remove_prefix(s):
    i = string.find(s, ":")
    if (i >=0): s = s[i+1:]
    return s    

# object_suffix is appended to each function for a JSObject
# 
#
# Thus, it needs to be appended to the constructor function declaration
# and every time the constructor is invoked with the "new" keyword
global object_suffix
object_suffix = "Obj"

global js_context
js_context = "window."

#########################################################################
#                        JSObject
#########################################################################
class JSObject:
    
    def __init__(self):
        self.name = "undefined"
        self.propertyNames = []   #array of strings
        self.propertyValues = []  #array of strings
        self.is_array = 0
        self.array_type = ""
        
    def add_property(self, name, value):
        i = string.find(value, ":")
        if (i >=0): value = value[i+1:]
        self.propertyNames.append(name)
        self.propertyValues.append(value)

    def num_properties(self):
        return len(self.propertyNames)

    def as_javascript(self):
        js_code = ""
        js_code += "function %s%s() {\n" % (self.name, object_suffix)
        if len(self.propertyNames) == 0:
            if (self.is_array):
                js_code += "\t// this is an array of type <%s%s>\n" % (self.array_type, object_suffix)
                js_code += "\tthis.items = new Array();\n"
#                js_code += "\tthis.toString = function() { str = '<%s>'; for (var i = 0; i < this.items.length; i++) { str += '<item>' + this.items[i].toString() + '</item>'; } return str + '</%s>'; };" % (self.name, self.name)
            else:
                js_code += "\t// no properties listed, wsdl2js failed for this type\n"
        else:
            toStr = "\"%sObj is \"" % self.name 
            for index in range(0, self.num_properties()):
                type = str(self.propertyValues[index])
                if (type in jsobjects):
                  jsobj = jsobjects[type]
                  type = type + object_suffix
                  if (jsobj.is_array): type = jsobj.array_type + object_suffix + "[]"
                defaultValue = "null"
                
                js_code += "\t /*%s*/ this[\"%s\"] = %s;\n" % (type, self.propertyNames[index], defaultValue)
                toStr += "+ \"%s=\" + this['%s'] + \", \" " % (self.propertyNames[index], self.propertyNames[index])
#            js_code += "\tthis.toString = function() {return %s;}\n" % toStr
        js_code += "}"
        return js_code

def create_jsobject_from_complexType(node):
    """create a JSObject from the <complexType> node"""
    jsobj = JSObject()
    
    name = node.getAttribute("name")
    jsobj.name = name
    
    elements = node.getElementsByTagNameNS("http://www.w3.org/2001/XMLSchema", "element")
    if (len(elements) == 0):
        # FIXME currently this assumes that only if there are no elements named "element"
        # then it may be an Array type
        # this probably doesn't characterize all Array types...
        # it works for GoogleSearch.wsdl, though!
        restrictions = node.getElementsByTagNameNS("http://www.w3.org/2001/XMLSchema", "restriction")
        for r in restrictions:
            base = r.getAttribute("base")
            base = remove_prefix(base)
            if (base == "Array"):
                jsobj.is_array = 1
                attributeNodes = r.getElementsByTagNameNS("http://www.w3.org/2001/XMLSchema", "attribute")
                for a in attributeNodes:
                    ref = a.getAttribute("ref")
                    if (ref and remove_prefix(ref) == "arrayType"):
                        typeAttr = a.getAttribute("arrayType")
                        typeAttr = a.getAttributeNS("http://schemas.xmlsoap.org/wsdl/", "arrayType")
                        jsobj.array_type = remove_prefix(typeAttr)[:-2]
    else:
        for e in elements:
            jsobj.add_property(e.getAttribute("name"), e.getAttribute("type"))
    return jsobj

def create_jsobject_from_message(node):
    """create a JSObject from the <message> node"""
    jsobj = JSObject()
    
    name = node.getAttribute("name")
    jsobj.name = name
    elements = node.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "part")
    for e in elements:
        jsobj.add_property(e.getAttribute("name"), e.getAttribute("type"))

    return jsobj

#########################################################################
#                        JSOperation
#########################################################################
class JSOperation:
    
    def __init__(self):
        self.name = "undefined"
        self.input = None #name of JSObject in jobjects
        self.inputNamespace = ""
        self.outputNamespace = ""
        self.transportURI = ""
        self.output = None #name of JSObject in jobjects
        
    def set_input(self, input):
        i = string.find(input, ":")
        if (i >= 0): input = input[i+1:]
        self.input = input
    
    def set_output(self, output):
        i = string.find(output, ":")
        if (i >= 0): output = output[i+1:]
        self.output = output
    
    def as_javascript(self):
        # if param and function have same name, replace name of param throughout
        param_name = self.input
        if (not self.input == None and self.input == self.name):
            param_name = self.input + "Param"
        
        params = []
        js_code = ""
        # create doc comment
        js_code += "/**\n"
        if (not self.input == None):
            js_code += " * @param %s of type %s%s\n" % (param_name, self.input, object_suffix)
        returnType = is_simple(self.output) or self.output
        if returnType == "base64Binary": returnType = "string"
        js_code += " * @return %s\n" % returnType
        js_code += " */\n"
        # create function signature
        js_code += "function %s(" % self.name
        if (not self.input == None):
            js_code += param_name
        js_code += ") {\n"
        
        js_code += "\tvar call = new %sSOAPCall();\n" % (js_context)
        js_code += "\tcall.transportURI = \"%s\";\n\n" % self.transportURI
        
        # create SOAPParameters
        if (self.input in jsobjects):
            jsobj = jsobjects[self.input]
            for i in range(0, jsobj.num_properties()):
                pr = "param" + str(i)
                name = jsobj.propertyNames[i]
                js_code += "\tvar %s = new %sSOAPParameter();\n" % (pr, js_context)
                js_code += "\t%s.name = \"%s\";\n" % (pr, name)            
                js_code += "\t%s.value = %s[\"%s\"];\n\n" % (pr, param_name, name)

        js_code += "\tvar myParamArray = ["
        if (self.input in jsobjects):
            jsobj = jsobjects[self.input]
            for i in range(0, jsobj.num_properties()):
                js_code += "param" + str(i)
                if ((i + 1) < jsobj.num_properties()): js_code += ", "
        js_code += "];\n"
        
        # invoke SOAP call
        js_code += "\tcall.encode(0, \"%s\", \"%s\", 0, null, myParamArray.length, myParamArray);\n" % (self.name, self.inputNamespace)
        js_code += "\tvar translation = call.invoke();\n"
       
        # error handling
        js_code += """
\tif (translation.fault) {
\t\t// error returned from the web service
\t\tthrow translation.fault;
\t} else {
"""        
        if (self.output == None):
            js_code += "\t\treturn;\n"
        elif (is_simple(self.output)):
            js_code += "\t\tvar response = translation.getParameters(false, {});\n"
            js_code += "\t\tvar value = response[0].value;\n"
            simpleType = is_simple(self.output)            
            if (simpleType == "int"):
              js_code += "\t\tif (value != null) value = %sparseInt(value, 10);\n" % (js_context)
            elif (simpleType == "double"):
              js_code += "\t\tif (value != null) value = %sparseFloat(value);\n" % (js_context)
            elif (simpleType == "boolean"):
              js_code += "\t\tif (value != null) value = (value == 'true');\n"
            elif (simpleType == "base64Binary"):
              js_code += "\t\tif (value != null) value = %satob(value);\n" % (js_context)
            js_code += "\t\treturn value;\n"
        else:
            output_type = jsobjects[self.output]
            js_code += "\t\tvar temp;\n"
            js_code += "\t\tvar obj0 = new %s%s();\n" % (self.output, object_suffix)
            js_code += "\t\tvar node0 = (translation.body.getElementsByTagName('%s'))[0];\n" % self.output
            
            for n, v in zip(output_type.propertyNames, output_type.propertyValues):
                js_code += extract_from_dom_and_assign("obj0", n, v, 0, 0)
                
            if (len(output_type.propertyNames) == 1):
              js_code += "\t\treturn obj0['%s'];\n" % (output_type.propertyNames[0])
            else:
              js_code += "\t\treturn obj0;\n"
            
        js_code += "\t}\n"
        
        # close entire function  
        js_code += "}"

        return js_code
                
def create_jsoperation(node):
    """create a JSOperation from the <operation> node"""
    jsopr = JSOperation()
    
    jsopr.name = node.getAttribute("name")
    
    input = node.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "input")
    if (not input == None and len(input) > 0):
        jsopr.set_input(input[0].getAttribute("message"))

    output = node.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "output")
    if (not output == None and len(output) > 0):
        jsopr.set_output(output[0].getAttribute("message"))
    
    return jsopr        

def extract_from_dom_and_assign(bean, propertyName, varType, depth, in_array):
    """
    Return the JavaScript necessary to extract a bean's property from an
    XML node in the SOAP response.
    
    parentNode: the node that has the element with the info as a child
    bean: the bean to which the value of the property is to be assigned
    propertyName: the name of the element/property
    varType: the type of the value, may be simple or complex
    in_array: True if this is a property within an array
    """
    tab = "\t\t" + (depth * "\t")
    if (is_simple(varType)):
        # use parseInt, parseFloat, parseBoolean, atob, as appropriate
        str = tab + "temp = node%d.getElementsByTagName('%s')[0].firstChild;\n" % (depth, propertyName)        
        str += tab + "%s['%s'] = (temp == null) ? null : " % (bean, propertyName)
        if (varType == 'int'):
          str += "%sparseInt(temp.nodeValue, 10);\n" % (js_context)
        elif (varType == 'double'):
          str += "%sparseFloat(temp.nodeValue);\n" % (js_context)
        elif (varType == 'boolean'):
          str += "(temp.nodeValue == 'true');\n"
        elif (varType == 'base64Binary'):
          str += "%satob(temp.nodeValue);\n" % (js_context)
        else:
          str += "temp.nodeValue;\n"
        return str
    childObj = jsobjects[varType]
    if (childObj.is_array):
        str = ""
        newBean = "obj%d" % (depth + 1)                 
#        str += tab + "var %s = new %s%s();\n" % (newBean, childObj.name, object_suffix)
        str += tab + "var %s = [];\n" % (newBean);
        str += tab + "var node%d = (node%d.getElementsByTagName('%s'))[0];\n" % (depth + 1, depth, propertyName)
        
        # stuff that is unique to Array
        str += tab + "// code for %s\n" % propertyName
        str += tab + "items%d = node%d.getElementsByTagName('item');\n" % (depth + 1, depth + 1)
        str += tab + "for (var i = 0; i < items%d.length; i++) {\n" % (depth + 1)
        # loop
        
        # TODO: Figure out what this should be doing 
        str += extract_from_dom_and_assign("obj%d" % (depth + 1), childObj.array_type, jsobjects[childObj.name].array_type, depth + 1, 1)
        
        # close loop
        str += tab + "}\n"

        str += tab + "%s['%s'] = %s;\n" % (bean, propertyName, newBean)
        return str
    else: 
        str = ""
        newBean = "obj%d" % (depth + 1)                 
        str += tab + "var %s = new %s%s();\n" % (newBean, childObj.name, object_suffix)
        if (in_array):
            str += tab + "var node%d = items%d[i];\n" % (depth + 1, depth)
        else:
            str += tab + "var node%d = (node%d.getElementsByTagName('%s'))[0];\n" % (depth + 1, depth, propertyName)
            
        for n, v in zip(childObj.propertyNames, childObj.propertyValues):
            str += extract_from_dom_and_assign(newBean, n, v, depth + 1, 0)
            
        if (in_array):
            str += tab + "%s.push(%s);\n" % (bean, newBean)
        else:
            str += tab + "%s['%s'] = %s;\n" % (bean, propertyName, newBean)
        return str

# the available command-line options
RECOGNIZED_OPTIONS= {
  "f:" : "specify the input .wsdl file, REQUIRED",
  "o:" : "specify the output .js file; prints to the console if unspecified"
}

def usage():
    """display how wsdl2js should be called"""
    print "usage error"
    print "here are the command-line options for wsdl2js.py:"
    for opt in RECOGNIZED_OPTIONS.keys():
        description = RECOGNIZED_OPTIONS[opt]
        if (opt[-1] == ':'): opt = opt[:-1]
        print "-%s : %s" % (opt, description)

# usage of getopt derived from:
# http://docs.python.org/lib/module-getopt.html
def check_opts():
    """verify that the command-line options are valid"""
    try:
        options = ""
        for opt in RECOGNIZED_OPTIONS.keys():
            options += opt
        return getopt.getopt(sys.argv[1:], options)
    except getopt.GetoptError:
        # print help information and exit
        usage()
        sys.exit(2)

def main():
    # get the command-line arguments
    opts, args = check_opts() 

    # check to see if an input file has been defined
    input_file = None
    for pair in opts:
        if (pair[0] == "-f"):
            input_file = pair[1]
    if input_file == None:
        print "failed to specify a .wsdl file, use -f"
        sys.exit(2)

    # parse .wsdl file
    dom = parse(input_file)

    # get the type node from the DOM and create a JSObject for each complexType    
    types = dom.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "types")
    complex_types = types[0].getElementsByTagNameNS("http://www.w3.org/2001/XMLSchema", "complexType")
    for t in complex_types:
        jsobj = create_jsobject_from_complexType(t)
        jsobjects[jsobj.name] = jsobj
    
    # get the message nodes from the DOM and create a JSObject for each message
    messages = dom.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "message")
    for m in messages:
        jsobj = create_jsobject_from_message(m)
        jsobjects[jsobj.name] = jsobj
    
    # generate the JavaScript for each JSObject
    output = "/* generated by wsdl2js.py (http://www.bolinfest.com/wsdl2js/) */\n\n"
    for t in jsobjects.values():
      if not t.is_array:
        output += t.as_javascript() + "\n\n"

    # get the transportURI
    address = dom.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/soap/", "address")
    transportURI = str(address[0].getAttribute("location"))

    # generate the JavaScript for each <operation>
    portType = dom.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "portType")
    operations = portType[0].getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "operation")
    binding = (dom.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "binding"))[0]    
    for o in operations:
        jsopr = create_jsoperation(o)
        jsopr.transportURI = transportURI
        # find the inputNamespace for each operation
        oprs = binding.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "operation")
        for op in oprs:
            if op.getAttribute("name") == jsopr.name:
                input = op.getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/", "input")
                if (not input == None and len(input) > 0):
                    body = input[0].getElementsByTagNameNS("http://schemas.xmlsoap.org/wsdl/soap/", "body")
                    jsopr.inputNamespace = body[0].getAttribute("namespace")
        output += jsopr.as_javascript() + "\n\n"

    # write the JavaScript to the console or to a file
    output_file = None
    # replace tabs with spaces
    output = output.replace('\t', '  ')
    for pair in opts:
        if (pair[0] == "-o"):
            output_file = pair[1]
    if output_file == None:
        print output
    else:
        file = open(output_file, "w")
        file.write(output)
        file.close()

if __name__ == "__main__":
    main()