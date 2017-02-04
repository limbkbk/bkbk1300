#include <list>

template<typename T>
class List : public Imp<T>
{
	std::list<T> l;

public:
	int length()
	{
		return l.size();
	}
	void clear()
	{
		l.clear();
	}
	void push_back(T t)
	{
		l.push_back(t);
	}
	void pop_front()
	{
		l.pop_front();
	}
	T front()
	{
		return l.front();
	}
};