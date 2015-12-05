package pack;

import java.util.Iterator;

public class MyLinkedList<T> implements Iterable<T>{

	private Element<T> value;

	public void add(T e) {
		Element<T> tmp = new Element<>(e);
		Element<T> current = value;

		if (value == null) {
			value = tmp;
			return;
		}

		while (current.getNext() != null) {
			current = current.getNext();
		}
		current.setNext(tmp);
	}

	public T get(int index) {
		Element<T> current = value;

		while (index > 0) {
			current = current.getNext();
			index--;
		}

		return current.getValue();
	}

	@Override
	public Iterator<T> iterator() {
		
		
		Iterator<T> iter = new Iterator<T>() {
			
			@Override
			public boolean hasNext(){
				return true;
			}

			@Override
			public T next() {
				return value.getValue();
			}

			@Override
			public void remove() {
				
			}
			
		};
		
		
		return iter;
	}
}
