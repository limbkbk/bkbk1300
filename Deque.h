#include <deque>

template<typename T>
class Deque : public Imp<T>
{
	std::deque<T> d;

public:
	int length()
	{
		return d.size();
	}
	void clear()
	{
		d.clear();
	}
	void push_back(T t)
	{
		d.push_back(t);
	}
	void pop_front()
	{
		d.pop_front();
	}
	T front()
	{
		return d.front();
	}
};