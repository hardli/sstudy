// ruihuaTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <cv.h>  
#include<highgui.h>
using namespace cv;
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	Mat src, src_gray;
    Mat grad;
    int scale = 1;
    int delta = 0;
    int ddepth = CV_16S;
    //![variables]

    //![load]
    src = imread("H:\\pic\\034_2014-07-04 13-12-53_35.jpg", IMREAD_COLOR); // Load an image
	resize(src,src,Size(src.cols/2,src.rows/2),0,0,INTER_LINEAR);  
    if (src.empty())
    {
        return -1;
    }
    //![load]

    //![reduce_noise]
    GaussianBlur(src, src, Size(3, 3), 0, 0, BORDER_DEFAULT);
    //![reduce_noise]

    //![convert_to_gray]
    cvtColor(src, src_gray, COLOR_BGR2GRAY);
    //![convert_to_gray]

    //![sobel]
    /// Generate grad_x and grad_y
    Mat grad_x, grad_y;
    Mat abs_grad_x, abs_grad_y;

    /// Gradient X
    //Scharr( src_gray, grad_x, ddepth, 1, 0, scale, delta, BORDER_DEFAULT );
    Sobel(src_gray, grad_x, ddepth, 1, 0, 3, scale, delta, BORDER_DEFAULT);

    /// Gradient Y
    //Scharr( src_gray, grad_y, ddepth, 0, 1, scale, delta, BORDER_DEFAULT );
    Sobel(src_gray, grad_y, ddepth, 0, 1, 3, scale, delta, BORDER_DEFAULT);
    //![sobel]

    //![convert]
    convertScaleAbs(grad_x, abs_grad_x);
    convertScaleAbs(grad_y, abs_grad_y);
    //![convert]

    //![blend]
    /// Total Gradient (approximate)
    addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, grad);
    //![blend]

    //![display]
    const char* window_name = "Sobel Demo - Simple Edge Detector";
    const char* window="Origin";
    imshow(window,src);
    imshow(window_name, grad);
	imwrite("E:\\pictest\\ruihuapic.jpg",grad);
    waitKey(0);

	/*string picName="H:\\pic\\034_2014-07-04 13-12-53_35.jpg";  
    Mat A=imread (picName,CV_LOAD_IMAGE_COLOR);  
    resize(A,A,Size(A.cols/2,A.rows/2),0,0,INTER_LINEAR);  
    Mat mask=(Mat_<char>(3,3)<<0,-1,0,  
                              -1,5,-1,  
                              0,-1,0);    
      
    Mat B;  
    filter2D (A,B,A.depth (),mask);  
      
    imshow("A的图像",A);  
    imshow("B的图像",B);  
    imwrite("E:\\ruihuapic.jpg",B);
    waitKey ();  */
	return 0;
}

