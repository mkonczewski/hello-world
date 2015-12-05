package pack;


public class MyLinkedList<T>{

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
}
