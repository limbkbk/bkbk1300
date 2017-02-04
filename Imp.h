template<class T>

class Imp
{
public:
	virtual int length() { return 0; }
	virtual void clear() { }
	virtual void push_back(T) { }
	virtual void pop_front() { }
	virtual T front() { return 0; }
};