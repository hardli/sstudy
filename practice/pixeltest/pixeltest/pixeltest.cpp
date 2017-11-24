// pixeltest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <stdio.h>   
#include <cv.h> 
using namespace cv;
using namespace std;  
  
IplImage* src = 0;     
IplImage* dst = 0;  
IplImage* xsrc = 0; 
void on_mouse( int event, int x, int y, int flags, void* ustc)    
{    
    static CvPoint pre_pt = {-1,-1};    
    static CvPoint cur_pt = {-1,-1};    
    CvFont font;    
    cvInitFont(&font, CV_FONT_HERSHEY_SIMPLEX, 0.5, 0.5, 0, 1, CV_AA);    
    char temp[16];    
        
    if( event == CV_EVENT_LBUTTONDOWN )    
    {    
        cvCopy(dst,xsrc);    
        sprintf(temp,"(%d,%d)",x,y);    
        pre_pt = cvPoint(x,y);    
        cvPutText(xsrc,temp, pre_pt, &font, cvScalar(0,0, 0, 255));    
        cvCircle( xsrc, pre_pt, 3,cvScalar(255,0,0,0) ,CV_FILLED, CV_AA, 0 );    
        cvShowImage( "xsrc", xsrc );    
        cvCopy(xsrc,dst);    
    }    
    else if( event == CV_EVENT_MOUSEMOVE && !(flags & CV_EVENT_FLAG_LBUTTON))    
    {    
        cvCopy(dst,xsrc);    
        sprintf(temp,"(%d,%d)",x,y);    
        cur_pt = cvPoint(x,y);          
        cvPutText(xsrc,temp, cur_pt, &font, cvScalar(0,0, 0, 255));    
        cvShowImage( "xsrc", xsrc );    
    }    
    else if( event == CV_EVENT_MOUSEMOVE && (flags & CV_EVENT_FLAG_LBUTTON))    
    {    
        cvCopy(dst,xsrc);    
        sprintf(temp,"(%d,%d)",x,y);    
        cur_pt = cvPoint(x,y);          
        cvPutText(xsrc,temp, cur_pt, &font, cvScalar(0,0, 0, 255));    
        cvRectangle(xsrc, pre_pt, cur_pt, cvScalar(0,255,0,0), 1, 8, 0 );    
        cvShowImage( "xsrc", xsrc );    
    }    
    else if( event == CV_EVENT_LBUTTONUP )    
    {    
        sprintf(temp,"(%d,%d)",x,y);    
        cur_pt = cvPoint(x,y);          
        cvPutText(xsrc,temp, cur_pt, &font, cvScalar(0,0, 0, 255));    
        cvCircle( xsrc, cur_pt, 3,cvScalar(255,0,0,0) ,CV_FILLED, CV_AA, 0 );    
        cvRectangle( xsrc, pre_pt, cur_pt, cvScalar(0,255,0,0), 1, 8, 0 );    
        cvShowImage( "xsrc", xsrc );    
        cvCopy(xsrc,dst);    
    }    
}    
int _tmain(int argc, _TCHAR* argv[])
{   
	CvSize czSize;              //目标图像尺寸 
    src=cvLoadImage("H:\\pic\\033_2014-07-04 13-12-44_34.jpg",1);    
   // dst=cvCloneImage(src);  
	xsrc = NULL;
	//计算目标图像大小  
    czSize.width = src->width * 0.5;  
    czSize.height = src->height * 0.5;
	xsrc = cvCreateImage(czSize, src->depth, src->nChannels);  
    cvResize(src, xsrc, CV_INTER_AREA); 
	dst=cvCloneImage(xsrc); 
    cvNamedWindow("xsrc",1);    
    cvSetMouseCallback( "xsrc", on_mouse, 0 );    
        
    cvShowImage("xsrc",xsrc);    
    
    cvWaitKey(0);     
    cvDestroyAllWindows();  
	cvReleaseImage(&src); 
    cvReleaseImage(&xsrc);    
    cvReleaseImage(&dst);    
    return 0;    
}    


