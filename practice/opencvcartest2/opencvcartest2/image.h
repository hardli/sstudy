#pragma once  
#include "cv.h"  
#include<highgui.h>  
class Image  
{  
public:  
    Image(void);  
    ~Image(void);  
    static void cloneImage(IplImage *src,IplImage*dst);  
    static void ZerosImage(IplImage * src);  
    static void dilateImage(IplImage* src, IplImage *dst);  
    static void erodeImage(IplImage* src,IplImage*dst);  
    static void rgb2gray(IplImage* src, IplImage* dst);  
    static void gray2bw(IplImage* src, IplImage* dst,unsigned char value);  
};  