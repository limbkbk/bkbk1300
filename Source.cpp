#include <string>
#include "Imp.h"
#include "List.h"
#include "Deque.h"
#include "Queue.h"

template <typename T>
void displayAndEmptyQueue(Queue<T> q)
{
	while (q.length() > 0)
	{
		cout << q.peek() << " ";
		q.pop();
	}
	cout << "\n" << endl;
}

int main() {
	// Test ints
	Deque<int> deq;
	Queue<int> q(&deq); // No default ctor, so we need to provide it here at init
	q.push(1);
	q.push(2);

	// Swap implementation
	List<int> lst;
	lst.push_back(3);
	q.changeStorage(&lst);
	q.push(4);
	q.push(5);
	displayAndEmptyQueue(q);

	// Test strings
	Deque<string> deq2;
	Queue<string> q2(&deq2);
	q2.push("1");
	q2.push("2");

	// Swap implementation
	List<string> lst2;
	lst2.push_back("3");
	q2.changeStorage(&lst2);
	q2.push("4");
	q2.push("5");
	displayAndEmptyQueue(q2);
}

/* Output:
1 2 4 5
1 2 4 5
*/