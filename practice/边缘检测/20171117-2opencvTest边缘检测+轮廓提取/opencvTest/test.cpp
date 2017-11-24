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
		//// ����һ��ͼƬ����Ϸԭ����  
		//Mat img=cvLoadImage("E:\\timg.jpg");   
		//// ����һ����Ϊ "��Ϸԭ��"����  
		//if(!(img.empty()))
		//{
		//	namedWindow("�羰ԭͼ");  
		//	// �ڴ�������ʾ��Ϸԭ��  
		//	imshow("�羰ԭͼ",img);  
		//	// �ȴ�6000 ms�󴰿��Զ��ر�  
		//	waitKey(6000);  
		//}

		//Mat src = imread("E:/timg.jpg");  //����Ŀ¼��Ӧ����һ����Ϊ1.jpg���ز�ͼ  
		//if(!(src.empty()))
		//{
		//	Canny(src, src, 3, 9,3 );  
		//	imshow("��Ч��ͼ��Canny��Ե���", src);  
		//	waitKey(); 
		//}

		//����ԭʼͼ    
		Mat src = imread("H:\\pic\\034_2014-07-04 13-12-53_35.jpg");  //����Ŀ¼��Ӧ����һ����Ϊ1.jpg���ز�ͼ  
		Mat dstImage1;
		resize(src,dstImage1,Size(src.cols/2,src.rows/2),0,0,INTER_LINEAR);  
		Mat src1=dstImage1.clone();  
        Mat src2=dstImage1.clone();  
		//��ʾԭʼͼ   
		//imshow("��ԭʼͼ��Canny��Ե���", src);   
		imshow("��С",dstImage1);  
  
		//----------------------------------------------------------------------------------  
		//  һ����򵥵�canny�÷����õ�ԭͼ��ֱ���á�  
		//----------------------------------------------------------------------------------  
		Canny( src2, src2, 150, 100,3 );  
		imshow("��Ч��ͼ��Canny��Ե���", src2); 
		imwrite("H:\\pictest\\test02.jpg",src2);
  
      
		//----------------------------------------------------------------------------------  
		//  �����߽׵�canny�÷���ת�ɻҶ�ͼ�����룬��canny����󽫵õ��ı�Ե��Ϊ���룬����ԭͼ��Ч��ͼ�ϣ��õ���ɫ�ı�Եͼ  
		//----------------------------------------------------------------------------------  
		Mat dst,edge,gray;  
  
		// ��1��������srcͬ���ͺʹ�С�ľ���(dst)  
		dst.create( src1.size(), src1.type() );  
  
		// ��2����ԭͼ��ת��Ϊ�Ҷ�ͼ��  
		cvtColor( src1, gray, CV_BGR2GRAY );  
  
		// ��3������ʹ�� 3x3�ں�������  
		//blur( gray, edge, Size(3,3) );  
		GaussianBlur(gray,edge,Size(5,5),0,0);
  
		// ��4������Canny����  
		Canny( edge, edge, 3, 9,3 );  
		vector<vector<Point>> contours; 
		findContours(edge, contours, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE);
		Mat result(edge.size(),CV_8U,Scalar(0)); 
		drawContours(result,contours,-1,Scalar(255),2); 
  
		////��5����g_dstImage�ڵ�����Ԫ������Ϊ0   
		//dst = Scalar::all(0);  
  //
		////��6��ʹ��Canny��������ı�Եͼg_cannyDetectedEdges��Ϊ���룬����ԭͼg_srcImage����Ŀ��ͼg_dstImage��  
		//src1.copyTo( dst, edge);  
        
		//��7����ʾЧ��ͼ   
		imshow("��Ч��ͼ��Canny��Ե���2", result);   
		imwrite("H:\\pictest\\test03.jpg",result);
  
  
		waitKey(0);   
  
		return 0;   

	}
	catch(cv::Exception& e)
	{
		printf("exception: %s\n",e.what());
	}
} 