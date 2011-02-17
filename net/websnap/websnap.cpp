#include <stdio.h>
#include "Video.h"
#include <Windows.h> // needed for 'sleep'
#include <Wininet.h> // needed for 'sleep'



const char * help()
{
	static char h[1024];
	sprintf( h, "use : websnap\n"
				"	  -host [ftphost address]\n"
				"	  -user [ftp user name]\n"
				"	  -pass [ftp password]\n"
				"	  -dir  [optional directory on your ftp server]\n"
				"	  -pic  [optional picture name(must be png!)]\n"
				"	  -vsrc [optional videosrc, can be 'camera', or 'camera_opt', if you want the dialog-box]\n"
				"	  -vdst [optional video dest, can be 'Video Renderer', or 'NullRenderer', if you dont want to watch.]\n"
				"	  -time [optional timeout betweeen pics in seconds.]\n" );
	return h;
}

HINTERNET net=0, con=0;
Video::Grabber * grabber = 0;
Video::FilterGraph * graph = 0;

void myexit()
{
	printf( "..bye!\n");
	if ( graph ) delete graph;
	if ( grabber ) delete grabber;
	if ( con ) InternetCloseHandle( con );
	if ( net ) InternetCloseHandle( net );
}

int main(int argc, char **argv)
{
	char * ftphost  = 0;
	char * ftpuser  = 0;
	char * ftppass  = 0;
	char * ftpdir   = "cam";
	char * pic      = "snap.png";
	char * vidsrc   = "camera"; // "camera_opt", if you want to control video props
	char * viddst   = "Video Renderer"; // "NullRenderer", if you don't want the window
	int  seconds    = 10;
	bool externalFTP = false;


	atexit(myexit);

	for ( int i=0; i<argc; i++ )
	{
		if ( ! strcmp(argv[i], "-host") )	ftphost = argv[++i];	
		if ( ! strcmp(argv[i], "-user") )	ftpuser = argv[++i];
		if ( ! strcmp(argv[i], "-pass") )	ftppass = argv[++i];
		if ( ! strcmp(argv[i], "-dir" ) )	ftpdir  = argv[++i];
		if ( ! strcmp(argv[i], "-pic" ) )	pic     = argv[++i];
		if ( ! strcmp(argv[i], "-vsrc" ) )	vidsrc	= argv[++i];
		if ( ! strcmp(argv[i], "-vdst" ) )	viddst  = argv[++i];		
		if ( ! strcmp(argv[i], "-time") )	seconds = atoi(argv[++i]);		
		if ( ! strcmp(argv[i], "-ext") )	externalFTP = true;;		
	}
	if ( !ftphost || !ftpuser || !ftppass )
	{
		printf( help() );
		return 1;
	}

	if ( externalFTP )
	{
		//
		// being lazy, i just remote-control ftp.exe...
		//
		// set up ftp - commands
		FILE * ft = fopen( "ftp.txt", "wb" );
		fprintf( ft, "%s\n%s\n", ftpuser, ftppass );
		if ( ftpdir )
		{
			fprintf( ft, "cd %s\n", ftpdir );
		}
		fprintf( ft, "binary\n" );
		fprintf( ft, "put %s\n", pic );
		fprintf( ft, "quit\n" );
		fclose( ft );
	}
	else
	{
		net = InternetOpen("", INTERNET_OPEN_TYPE_DIRECT,"","",0);
		if ( ! net )
		{
			return 2;
		}

		con = InternetConnect( net, ftphost,21,ftpuser,ftppass,INTERNET_SERVICE_FTP,0,0);
		if ( ! con )
		{
			return 3;
		}

		if ( ftpdir )
		{
			FtpSetCurrentDirectory( con, ftpdir );
		}
	}
	// create & start filtergraph:
	grabber = Video::createGrabber(pic);
	if ( ! grabber )
	{
		return 4;
	}
	graph = Video::createFilterGraph();
	if ( ! graph )
	{
		return 5;
	}

	bool ok = graph->build( vidsrc, grabber, viddst );
	if( ok )
	{
		ok = graph->start();
		while( ok )
		{
			grabber->snap();
			Sleep(seconds * 1000); 
			if ( externalFTP )
			{
				char cmdline[300];
				sprintf( cmdline, "ftp -s:ftp.txt %s", ftphost );
				system( cmdline );
			}
			else
			{
				FtpPutFile( con, pic, pic, FTP_TRANSFER_TYPE_BINARY, 0 );
			}
		}
	}
	return 0;
}
