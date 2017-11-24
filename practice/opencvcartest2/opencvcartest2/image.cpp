
#include "stdafx.h"
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
  
Image::Image(void)  
{  
}  
  
  
Image::~Image(void)  
{  
}  
  
void Image::cloneImage(IplImage *src,IplImage*dst){  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    if (nChannels == 1)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep+j;  
                pPixel2 = (unsigned char *)dst->imageData + i*dst->widthStep+j;   
                pPixel2[0] = pPixel[0];                            
            }  
        }  
    }else if (nChannels == 3)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep+3*j;  
                pPixel2 = (unsigned char *)dst->imageData + i*dst->widthStep+3*j;   
                pPixel2[0] = pPixel[0];  
                pPixel2[1] = pPixel[1];  
                pPixel2[2] = pPixel[2];                              
            }  
        }  
    }  
}  
void Image::ZerosImage(IplImage * src){  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    if (nChannels == 1)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep+j;  
                pPixel[0] = 0;                            
            }  
        }  
    }else if (nChannels == 3)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep + 3*j;   
                pPixel[0] = 0;  
                pPixel[1] = 0;  
                pPixel[2] = 0;                              
            }  
        }  
    }  
  
}  
//ÅòÕÍ  
void Image::dilateImage(IplImage* src, IplImage *dst){  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    IplImage * tmpDst = cvCreateImage(cvSize(imageWidth,imageHeight),src->depth,src->nChannels);  
    ZerosImage(tmpDst);  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    CvSize windowSize = cvSize(3,3);  
    int SW2 = windowSize.width/2;  
    int SH2 = windowSize.height/2;  
  
    if (nChannels == 1)  
    {  
        for (int i = 1; i< imageHeight - 1;i++)  
        {  
            for (int j = 1; j< imageWidth - 1 ;j++)  
            {  
                pPixel  = (unsigned char *)tmpDst->imageData + i*tmpDst->widthStep+j;  
                for (int m = i - SH2 ; m <= i+SH2; m++)  
                {  
                    for (int n = j - SW2; n <= j+SW2;n++)  
                    {  
                        pPixel2  = (unsigned char *)src->imageData + m*src->widthStep + n;  
                        if (pPixel2[0] == 255)  
                        {  
                            pPixel[0] = 255;  
                        }  
                    }  
                }  
            }  
        }  
    }else if (nChannels == 3)  
    {  
       //²»ÊÇ»Ò¶ÈÍ¼Ïñ£¬±¨´í  
    }  
  
    //¿½±´Í¼Ïñ  
    cloneImage(tmpDst,dst);  
    //cvShowImage("dst",dst);  
    //cvWaitKey(0);  
}  
void Image::erodeImage(IplImage* src,IplImage*dst){  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    IplImage * tmpDst = cvCreateImage(cvSize(imageWidth,imageHeight),src->depth,src->nChannels);  
    ZerosImage(tmpDst);  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    CvSize windowSize = cvSize(3,3);  
    int SW2 = windowSize.width/2;  
    int SH2 = windowSize.height/2;  
    int flag = 0;  
  
    if (nChannels == 1)  
    {  
        for (int i = 1; i< imageHeight - 1;i++)  
        {  
            for (int j = 1; j< imageWidth - 1 ;j++)  
            {  
                pPixel  = (unsigned char *)tmpDst->imageData + i*tmpDst->widthStep+j;  
                flag = 0;  
                for (int m = i - SH2 ; m <= i+SH2; m++)  
                {  
                    for (int n = j - SW2; n <= j+SW2;n++)  
                    {  
                        pPixel2  = (unsigned char *)src->imageData + m*src->widthStep + n;  
                        if (pPixel2[0] == 0)  
                        {  
                            flag = 1;  
                        }  
                    }  
                    if (flag == 0)  
                    {  
                        //  
                        pPixel[0] = 255;  
                    }else {  
                        pPixel[0] = 0;  
                    }  
  
                }  
            }  
        }  
    }else if (nChannels == 3)  
    {  
        //²»ÊÇ»Ò¶ÈÍ¼Ïñ£¬±¨´í  
    }  
  
    //¿½±´Í¼Ïñ  
    cloneImage(tmpDst,dst);  
    //cvShowImage("dst",dst);  
    //cvWaitKey(0);  
}  
  
void Image::rgb2gray(IplImage* src, IplImage* dst){  
    //Gray = 0.29900 * R + 0.58700 * G + 0.11400 * B  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    unsigned char R ,G, B;  
    unsigned char grayPixle = 0;  
  
    if (nChannels == 3)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep+j*3;  
                pPixel2 = (unsigned char *)dst->imageData + i*dst->widthStep+j;  
                B = pPixel[0];  
                G = pPixel[1];  
                R = pPixel[2];  
                grayPixle = (unsigned char)(0.29900 * R + 0.58700 * G + 0.11400 * B);  
                pPixel2[0] = grayPixle;                            
            }  
        }  
    }else if (nChannels == 1)  
    {  
        //²»ÊÇRGBÍ¼Ïñ£¬±¨´í  
    }  
}  
void Image::gray2bw(IplImage* src, IplImage* dst,unsigned char value){  
    int nChannels = src->nChannels;  
    int imageHeight = src->height;  
    int imageWidth  = src->width;  
    unsigned char *pPixel= NULL;  
    unsigned char *pPixel2= NULL;  
    if (nChannels == 1)  
    {  
        for (int i = 0; i< imageHeight;i++)  
        {  
            for (int j = 0; j< imageWidth ;j++)  
            {  
                pPixel  = (unsigned char *)src->imageData + i*src->widthStep+j;  
                pPixel2  = (unsigned char *)dst->imageData + i*dst->widthStep+j;  
                if (pPixel[0] > value)  
                {  
                    pPixel2[0] = 255;  
                }else {  
                    pPixel2[0] = 0;  
                }  
                                            
            }  
        }  
    }else if (nChannels == 3)  
    {  
       //²»ÊÇ»Ò¶ÈÍ¼Ïñ£¬±¨´í  
    }  
}  