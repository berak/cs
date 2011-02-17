
#include "Fgraph.h"
#include "Video.h"
//#include "twaini.h"
#include <qedit.h>
#include <math.h>
//#include <stdio.h>

// Warnung	1	warning C4996: 'stricmp' wurde als veraltet deklariert	e:\code\e6\src\video\video.cpp	244
#pragma warning (disable:4996)


#include "png.h"


void my_png_error_ptr(png_structp, png_const_charp)
{}

int write_png(const char *file_name, char *bytes, int width, int height)
{
	FILE *fp;
	png_structp png_ptr;
	png_infop info_ptr;

	fp = fopen(file_name, "wb");
	if (fp == NULL)
	  return (0);
	png_ptr = png_create_write_struct(PNG_LIBPNG_VER_STRING, 0,0,0);

	if (png_ptr == NULL)
	{
	  fclose(fp);
	  return (0);
	}
	info_ptr = png_create_info_struct(png_ptr);
	if (info_ptr == NULL)
	{
	  fclose(fp);
	  png_destroy_write_struct(&png_ptr,  png_infopp_NULL);
	  return (0);
	}

	if (setjmp(png_jmpbuf(png_ptr)))
	{
	  /* If we get here, we had a problem reading the file */
	  fclose(fp);
	  png_destroy_write_struct(&png_ptr, &info_ptr);
	  return (0);
	}


	png_init_io(png_ptr, fp);
	png_set_IHDR(png_ptr, info_ptr, width, height, 8, PNG_COLOR_TYPE_RGB,
	  PNG_INTERLACE_NONE, PNG_COMPRESSION_TYPE_BASE, PNG_FILTER_TYPE_BASE);

	png_write_info(png_ptr, info_ptr);

	/* flip BGR pixels to RGB */
	png_set_bgr(png_ptr);

	png_bytep * row_pointers = new png_bytep[height];

	if (height > PNG_UINT_32_MAX/png_sizeof(png_bytep))
	 png_error (png_ptr, "Image is too tall to process in memory");

	// updown flipped:
	for (int k = 0; k < height; k++)
	 row_pointers[k] = (png_bytep)(bytes + (height-k-1)*width*3);

	png_write_image(png_ptr, row_pointers);
	png_write_end(png_ptr, info_ptr);

	png_destroy_write_struct(&png_ptr, &info_ptr);
	fclose(fp);

	delete [] row_pointers;
	return (1);
}


namespace Video
{

	// red rec led:
	void blink ( void *pData, int w, int red=255 ) 
	{
		if (  red < 5 ) return ;

	    static int pt[40][2] = 
		{
	                             {3,0}, {4,0},
	                      {2,1}, {3,1}, {4,1}, {5,1},
	               {1,2}, {2,2}, {3,2}, {4,2}, {5,2}, {6,2},
	        {0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3},
	        {0,4}, {1,4}, {2,4}, {3,4}, {4,4}, {5,4}, {6,4}, {7,4},
	               {1,5}, {2,5}, {3,5}, {4,5}, {5,5}, {6,5},
	                      {2,6}, {3,6}, {4,6}, {5,6},
	                             {3,7}, {4,7},
	    };
		
		struct _bgr {unsigned char b,g,r;} *prgb = (_bgr*) pData;

	    for ( int n=0; n<40; n++ ) 
		{
	        int k = pt[n][0] + pt[n][1] * w;
	        prgb[k].r = red;
	    }
	}

	void printGUID( const GUID & ng2 )
	{
	   printf_s(  "{%x08-%x04-%x04-", ng2.Data1, ng2.Data2, ng2.Data3 );
	   for (int i = 0 ; i < 8 ; i++) {
		  if (i == 2)
			 printf_s("-");
		  printf_s("%02x", ng2.Data4[i]);
	   }
	   printf_s("}\n");
	}

	
	class Sampler : public ISampleGrabberCB 
	{
		Grabber * grabber;
	public:
		int lWidth, lHeight; 

		Sampler() : grabber(0), lWidth(240),lHeight(160) 
		{
		}

		~Sampler() 
		{
		}

		void setGrabber( Grabber * grab )
		{
			this->grabber = grab;
		}
		void stop()
		{
			this->grabber = 0;
		}

		STDMETHODIMP_(ULONG) AddRef() { return 2; }
		STDMETHODIMP_(ULONG) Release() { return 1; }

		STDMETHODIMP QueryInterface(REFIID riid, void ** ppv)
		{
			if (riid == IID_ISampleGrabberCB || riid == IID_IUnknown) 
			{
				*ppv = (void *) static_cast<ISampleGrabberCB *>(this);
				return NOERROR;
			}    
			return E_NOINTERFACE;
		}

	    
		STDMETHODIMP SampleCB( double SampleTime, IMediaSample *pSample )
		{
			BYTE *pData;
			HRESULT hr = pSample->GetPointer(&pData);
			if ( hr == S_OK && grabber )
			{ 
				int ll = pSample->GetActualDataLength();
				int l = pSample->GetSize();
				int nb = 3 * this->lWidth*this->lHeight;
				//assert( l == nb );
				int res = grabber->processBGR( pData, this->lWidth, this->lHeight );
			}
			return hr;
		}

	    
		STDMETHODIMP BufferCB( double SampleTime, BYTE * pBuffer, long BufferLen )
		{
			return 0;
		}

	}; // Sampler





	struct CFilterGraph 
		: Video::FilterGraph
	{
		FGraph graph;
		Sampler sampler;

		CFilterGraph()
		{
		    CoInitialize(NULL);
			graph.setup();
		}
		virtual ~CFilterGraph()
		{
			graph.clear();
			sampler.stop();
			CoUninitialize();
		}


		IBaseFilter *  addFilter(IBaseFilter * src, const char * guidstr, const char * name)
		{
			HRESULT hr = S_OK;
			if ( ! src ) return 0;

			IBaseFilter * filter = graph.createFilter( (char*)guidstr, (char*)name );
			if ( filter )
			{
				// there might be audio, too, so start testing at last pin:
				hr = graph.connect( src, filter, 1 );
				if ( hr == S_OK )
				{
					return filter;
				}
				hr = graph.connect( src, filter, 0 );
				if ( hr == S_OK )
				{
					return filter;
				}
				// connection to src pin failed:
				char szn[200];
				graph.filterName(src,szn);
				char szx[200];
				sprintf(szx,"could not connect %s (%s) to filter!", name, guidstr );
				MessageBoxA( 0, szx,szn,0);
				graph.remove( filter );
				TL_RELEASE( filter );
			}
			return src;
		}

		// decompressors:
		IBaseFilter *  addAviDecomp(IBaseFilter * src)
		{
			return addFilter( src, "{CF49D4E0-1115-11CE-B03A-0020AF0BA770}", "avi-decomp" );
		}
		IBaseFilter *  addWmvDecomp(IBaseFilter * src)
		{
			return addFilter( src, "{63F8AA94-E2B9-11D0-ADF6-00C04FB66DAD}", "wmv-decomp" );
		}
		IBaseFilter *  addMpgDecomp(IBaseFilter * src)
		{
			IBaseFilter * tail = addFilter( src, "{336475D0-942A-11CE-A870-00AA002FEAB5}", "mpeg-splitter" );
			return addFilter( tail, "{FEB50740-7BEF-11CE-9BD9-0000E202599C}", "mpeg-decomp" );
		}
		IBaseFilter *  addCConverter( IBaseFilter * src )
		{
			return addFilter( src, "{1643E180-90F5-11CE-97D5-00AA0055595A}", "color-converter" );
		}


		// sinks:
		IBaseFilter *  addNullRenderer( IBaseFilter * src )
		{
			return addFilter( src, "{C1F400A4-3F08-11D3-9F0B-006008039E37}", "NullRenderer" );
		}


		// sources:
		IBaseFilter *  addColorSource()
		{
			IBaseFilter * src = graph.createFilter( "{0cfdd070-581a-11d2-9ee6-006008039e37}", "ColorSource" );

			//
			// i can't get the IAMTimelineObj.
			//
			////DWORD           dwYellow = 0xFFFF00;
			////IAMTimelineObj  *pSource = NULL;
			////IPropertySetter *pProp = NULL;

			////// Create a property setter.
			////CoCreateInstance(CLSID_PropertySetter, NULL, CLSCTX_INPROC_SERVER, 
			////	IID_IPropertySetter, (void**) &pProp);

			////// Set the color.
			////DEXTER_PARAM param;
			////DEXTER_VALUE val;

			////param.Name = SysAllocString(OLESTR("Color"));
			////param.dispID = 0;
			////param.nValues = 1;

			////val.v.vt = VT_I4;
			////val.v.lVal = dwYellow;
			////val.rt = 0;  // Time must be zero.
			////val.dwInterp = DEXTERF_JUMP;

			////pProp->AddProp(param, &val);
			////src->SetPropertySetter(pProp);

			////// Clean up.
			////SysFreeString(param.Name);
			////VariantClear(&val.v);
			////pProp->Release();

			return src;
		}

		IBaseFilter *  addCamera( bool showPropSheet=true )
		{
			IBaseFilter * cam = graph.getCam();
			if ( cam && showPropSheet )
			{
				IPin * pin =0;
				// our pin might not be the first (audio,preview):
				if ( S_OK != graph.getPin( cam,PINDIR_OUTPUT,0,&pin) )
					if ( S_OK != graph.getPin( cam,PINDIR_OUTPUT,1,&pin) )
						pin = 0;
				if ( pin )
					graph.displayPinProperties(pin,GetActiveWindow());

				TL_RELEASE(pin);
			}
			// we need 24-bit uncompressed input , so insert decomp before grabber:
			return ( addAviDecomp( cam ) );
		}

		IBaseFilter *  addFile( const char * srcName )
		{
			HRESULT hr = S_OK;
			IBaseFilter * pSource = 0;
			WCHAR wsz[180];
			a2w( srcName, wsz );    
			hr = graph.getGraph()->AddSourceFilter( wsz, wsz, &pSource );
			if ( hr != S_OK ) return 0;

			if ( strstr( srcName, ".wmv" ) )
				return addWmvDecomp( pSource );
			if ( strstr( srcName, ".avi" ) )
				return addAviDecomp( pSource );
			if ( strstr( srcName, ".mpg" ) )
				return addMpgDecomp( pSource );
			if ( strstr( srcName, ".mpeg" ) )
				return addMpgDecomp( pSource );
			return pSource;
		}


		IBaseFilter * addGrabber( Grabber * grab,  IBaseFilter * tail )
		{
			IBaseFilter * sample = graph.createFilter( "{C1F400A0-3F08-11d3-9F0B-006008039E37}", "SampleGrabber" );

			if ( ! sample ) 
			{
				printf( __FUNCTION__ " : Could not create SampleGrabber !\n" );
				return 0;
			}

			HRESULT hr = 0;
            ISampleGrabber  *sampleGrabber = 0;
            TL_QUERY( sample, IID_ISampleGrabber, sampleGrabber );
			while ( sampleGrabber )
			{
				// request rgb: ( do this BEFORE CONNECTING ! )
				{
					AM_MEDIA_TYPE mt = {0};
					mt.majortype = MEDIATYPE_Video;
					mt.subtype   = MEDIASUBTYPE_RGB24;
					hr = sampleGrabber->SetMediaType( &mt );
				}

				// connect:
				{
					hr = graph.connect( tail, sample, 1 );
					if ( hr != S_OK )
					{
						hr = graph.connect( tail, sample, 0 );
						if ( hr != S_OK )
						{
							char szn[200];
							graph.filterName(tail,szn);
							char szx[200];
							sprintf(szx,"could not connect to SampleGrabber!" );
							MessageBoxA( 0, szx,szn,0);
							graph.remove( sample );
							TL_RELEASE( sample );
							break;
						}
					}
				}

				// check mediatype again:
				AM_MEDIA_TYPE mt;
				hr = sampleGrabber->GetConnectedMediaType( &mt );

				if ( hr != S_OK )
				{
					printf( __FUNCTION__ " : no mediatape !\n" ); 
					break; 
				}
				if ( mt.majortype != MEDIATYPE_Video ) 
				{
					printf( __FUNCTION__ " : majortape != video!\n" ); 
					break; 
				}
				if ( mt.subtype   != MEDIASUBTYPE_RGB24 ) 
				{
					printf( "subtype : " ); 
					printGUID( mt.subtype );
					printf( __FUNCTION__ " : minortape != rgb24! \n" ); 
					break;
				}

				VIDEOINFOHEADER * vih = (VIDEOINFOHEADER*) mt.pbFormat;
				sampler.lWidth  = vih->bmiHeader.biWidth;
				sampler.lHeight = vih->bmiHeader.biHeight;
//				assert( (sampler.lWidth<4012) && (sampler.lHeight<4012) );
				sampler.setGrabber( grab );
				hr = sampleGrabber->SetCallback( &sampler, 0 );
				hr = sampleGrabber->SetOneShot(FALSE);
				hr = sampleGrabber->SetBufferSamples(FALSE);

				break; // done ok.
			}
            TL_RELEASE(sampleGrabber);
//            TL_RELEASE(sample);
			return sample;
		}


		virtual bool build(  const char * srcName, Grabber * grab, const char * dstName )
		{
			HRESULT hr = S_OK;
			IBaseFilter * tail = 0;

			if ( ! stricmp( srcName, "camera_opt" ) )
			{
				tail = addCamera(true);
			}
			else
			if ( ! stricmp( srcName, "camera" ) )
			{
				tail = addCamera(false);
			}
			else
			if ( ! stricmp( srcName, "Color" ) )
			{
				tail = addColorSource(); // black ;- as long as i don't know how to set the color..
			}
			else
			{
				tail = addFile(srcName);
			}

			if ( ! tail ) return 0;

			if ( grab )
			{
				tail = addGrabber( grab, tail );
			}

			if ( ! strcmp( dstName, "NullRenderer" ) )
			{
				tail = addNullRenderer( tail );
			}
			else // "Video Renderer"
			{
				hr = graph.render( tail, 1 );
				if ( hr != S_OK )
				{
					hr = graph.render( tail, 0 );
				}
			}

			TL_RELEASE( tail );
			return ( !hr );
		}


		virtual bool start() 
		{
			bool ok = graph.isValid();
			if ( ok )
			{
				ok = graph.play();
			}
			return ok;
		}
		virtual bool pause() 
		{
			bool ok = graph.isValid();
			if ( ok )
			{
				ok = graph.pause();
			}
			return ok;
		}
		virtual bool stop() 
		{
			bool ok = graph.isValid();
			if ( ok )
			{
				ok = graph.stop();
			}
			return ok;
		}
		virtual bool wind( float toPos ) 
		{
			bool ok = graph.isValid();
			if ( ok )
			{
				ok = graph.wind(toPos);
			}
			return ok;
		}

	};  // CFilterGraph



	struct CGrabber 
		: Video::Grabber
	{
		const char * fileName;
		bool doSnap;

		CGrabber(const char * fn) 
			: fileName(fn)
			, doSnap(0)
		{  
		}

		~CGrabber() 
		{
		}

		virtual bool processBGR( void * pixel, int w, int h ) 
		{	
			if ( doSnap )
			{
				write_png( fileName, (char*)pixel, w,h );
				doSnap = 0;
			}
			return 1;
		}
		virtual void snap()
		{
			doSnap = 1;
		}
	}; // CGrabber

	Grabber * createGrabber(const char*s) { return new CGrabber(s); }
	FilterGraph * createFilterGraph() {  return new CFilterGraph; }

} // video


