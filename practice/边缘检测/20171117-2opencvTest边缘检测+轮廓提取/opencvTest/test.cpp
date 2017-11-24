#pragma comment(lib,"opencv_highgui249d.lib")
#include<iostream>  
#include <opencv2/opencv.hpp>  
#include<opencv2/highgui/highgui.hpp>  
#include<opencv2/imgproc/imgproc.hpp> 
  
  
using namespace cv;  
  
  
int main()  
{  
	try
	{
		//// 读入一张图片（游戏原画）  
		//Mat img=cvLoadImage("E:\\timg.jpg");   
		//// 创建一个名为 "游戏原画"窗口  
		//if(!(img.empty()))
		//{
		//	namedWindow("风景原图");  
		//	// 在窗口中显示游戏原画  
		//	imshow("风景原图",img);  
		//	// 等待6000 ms后窗口自动关闭  
		//	waitKey(6000);  
		//}

		//Mat src = imread("E:/timg.jpg");  //工程目录下应该有一张名为1.jpg的素材图  
		//if(!(src.empty()))
		//{
		//	Canny(src, src, 3, 9,3 );  
		//	imshow("【效果图】Canny边缘检测", src);  
		//	waitKey(); 
		//}

		//载入原始图    
		Mat src = imread("H:\\pic\\034_2014-07-04 13-12-53_35.jpg");  //工程目录下应该有一张名为1.jpg的素材图  
		Mat dstImage1;
		resize(src,dstImage1,Size(src.cols/2,src.rows/2),0,0,INTER_LINEAR);  
		Mat src1=dstImage1.clone();  
        Mat src2=dstImage1.clone();  
		//显示原始图   
		//imshow("【原始图】Canny边缘检测", src);   
		imshow("缩小",dstImage1);  
  
		//----------------------------------------------------------------------------------  
		//  一、最简单的canny用法，拿到原图后直接用。  
		//----------------------------------------------------------------------------------  
		Canny( src2, src2, 150, 100,3 );  
		imshow("【效果图】Canny边缘检测", src2); 
		imwrite("H:\\pictest\\test02.jpg",src2);
  
      
		//----------------------------------------------------------------------------------  
		//  二、高阶的canny用法，转成灰度图，降噪，用canny，最后将得到的边缘作为掩码，拷贝原图到效果图上，得到彩色的边缘图  
		//----------------------------------------------------------------------------------  
		Mat dst,edge,gray;  
  
		// 【1】创建与src同类型和大小的矩阵(dst)  
		dst.create( src1.size(), src1.type() );  
  
		// 【2】将原图像转换为灰度图像  
		cvtColor( src1, gray, CV_BGR2GRAY );  
  
		// 【3】先用使用 3x3内核来降噪  
		//blur( gray, edge, Size(3,3) );  
		GaussianBlur(gray,edge,Size(5,5),0,0);
  
		// 【4】运行Canny算子  
		Canny( edge, edge, 3, 9,3 );  
		vector<vector<Point>> contours; 
		findContours(edge, contours, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE);
		Mat result(edge.size(),CV_8U,Scalar(0)); 
		drawContours(result,contours,-1,Scalar(255),2); 
  
		////【5】将g_dstImage内的所有元素设置为0   
		//dst = Scalar::all(0);  
  //
		////【6】使用Canny算子输出的边缘图g_cannyDetectedEdges作为掩码，来将原图g_srcImage拷到目标图g_dstImage中  
		//src1.copyTo( dst, edge);  
        
		//【7】显示效果图   
		imshow("【效果图】Canny边缘检测2", result);   
		imwrite("H:\\pictest\\test03.jpg",result);
  
  
		waitKey(0);   
  
		return 0;   

	}
	catch(cv::Exception& e)
	{
		printf("exception: %s\n",e.what());
	}
} 