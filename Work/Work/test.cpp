#include <iostream>
#include <string>
using namespace std;

class Employee {
public:
	string name;
	int age; 
	void overridingTest()
	{
		cout << "����� �̸��� " << name << "�̰�, ���̴� " << age << "�Դϴ�" << endl;
	}
};

class Manager : public Employee {
public:
	string jobId;

	void overridingTest()
	{
		cout << "����� �̸��� " << name << "�̰�, ���̴� " << age << "�Դϴ�" << endl;
		cout << "���� ������ " << jobId << "�Դϴ�" << endl;
	}
};

int main()
{
	Manager test;
	test.name = "���İ�";
	test.age = 26;
	test.jobId = "���� ���α׷���";
	test.overridingTest();
}



//class OverloadingTestClass {
//public:
//
//	void overloadingTest()
//	{
//		cout << "�Ű������� ���� �޼ҵ�" << endl;
//	}
//	void overloadingTest(int a)
//	{
//		cout << "�Ű������� " << a << "�� �޼ҵ�" << endl;
//	}
//	void overloadingTest(int a, int b)
//	{
//		cout << "�Ű������� " << a << "�� " << b << "�� �޼ҵ�" << endl;
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