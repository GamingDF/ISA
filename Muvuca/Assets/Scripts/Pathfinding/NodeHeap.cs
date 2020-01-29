using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHeap {
	Node[] items;
	int currentItemCount;

	public NodeHeap(int maxHeapSize) {
		items = new Node[maxHeapSize];
	}

	public void Add(Node item) {
		item.heapIndex = currentItemCount;
		items[currentItemCount] = item;
		SortUp(item);
		currentItemCount++;
	}

	public Node RemoveFirst() {
		Node firstItem = items[0];
		currentItemCount--;
		items[0] = items[currentItemCount];
		items[0].heapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}

	public bool Contains(Node item) {
		return Equals(items[item.heapIndex], item);
	}

	public void UpdateItem(Node item) {
		SortUp(item);
	}

	public int Count {
		get {
			return currentItemCount;
		}
	}

	void Swap(Node itemA, Node itemB) {
		items[itemA.heapIndex] = itemB;
		items[itemB.heapIndex] = itemA;
		int itemAIndex = itemA.heapIndex;
		itemA.heapIndex = itemB.heapIndex;
		itemB.heapIndex = itemAIndex;
	}

	void SortUp(Node item) {
		int parentIndex = (item.heapIndex-1)/2;

		while(true) {
			Node parentItem = items[parentIndex];
			if(item.CompareTo(parentItem) > 0) {
				Swap(item, parentItem);
			}
			else {
				return;
			}

			parentIndex = (item.heapIndex-1)/2;
		}
	}

	void SortDown(Node item) {
		while(true) {
			int childIndexLeft = item.heapIndex*2 + 1;
			int childIndexRight = item.heapIndex*2 + 2;
			int swapIndex = 0;

			if(childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;

				if(childIndexRight < currentItemCount) {
					if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}

				if(item.CompareTo(items[swapIndex]) < 0) {
					Swap(item, items[swapIndex]);
				}
				else {
					return;
				}
			}
			else {
				return;
			}
		}
	}
}
