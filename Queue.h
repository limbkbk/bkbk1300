#include <string>
#include <iostream>

using namespace std;

template<typename T>
class Queue 
{
	Imp<T>* I = nullptr;
public:
	Queue(Imp<T>* qi);
	~Queue();
	void push(T);	
	T peek();	
	void pop();
	size_t length() const;
	void clear();
	void changeStorage(Imp<T>* qi);
};

template<typename T>
Queue<T>::Queue(Imp<T>* qi)
{
	I = qi;
};

template<typename T>
Queue<T>::~Queue() {}

template<typename T>
void Queue<T>::push(T t)
{
	I->push_back(t);
}

template<typename T>
void Queue<T>::pop()
{
	I->pop_front();
}

template<typename T>
T Queue<T>::peek()
{
	return I->front();
}

template<typename T>
size_t Queue<T>::length() const
{
	return I->length();
}

template<typename T>
void Queue<T>::clear()
{
	I->clear();
}

template<typename T>
void Queue<T>::changeStorage(Imp<T>* qi)
{
	qi->clear();
	while (I->length()>0)
	{
		qi->push_back(I->front());
		I->pop_front();
	}
	I = qi;
}