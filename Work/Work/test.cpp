#include <iostream>
#include <string>
using namespace std;

class Employee {
public:
	string name;
	int age; 
	void overridingTest()
	{
		cout << "사원의 이름은 " << name << "이고, 나이는 " << age << "입니다" << endl;
	}
};

class Manager : public Employee {
public:
	string jobId;

	void overridingTest()
	{
		cout << "사원의 이름은 " << name << "이고, 나이는 " << age << "입니다" << endl;
		cout << "그의 직업은 " << jobId << "입니다" << endl;
	}
};

int main()
{
	Manager test;
	test.name = "이컴공";
	test.age = 26;
	test.jobId = "게임 프로그래머";
	test.overridingTest();
}



//class OverloadingTestClass {
//public:
//
//	void overloadingTest()
//	{
//		cout << "매개변수가 없는 메소드" << endl;
//	}
//	void overloadingTest(int a)
//	{
//		cout << "매개변수가 " << a << "인 메소드" << endl;
//	}
//	void overloadingTest(int a, int b)
//	{
//		cout << "매개변수가 " << a << "와 " << b << "인 메소드" << endl;
//	}
//};
//
//int main()
//{
//	OverloadingTestClass Test;
//	Test.overloadingTest();
//	Test.overloadingTest(100);
//	Test.overloadingTest(100, 200);
//}