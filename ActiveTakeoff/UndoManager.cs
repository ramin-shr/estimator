using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	internal class UndoManager
	{
		public UndoManager()
		{
			this.ClearHistory();
		}

		public bool CanUndo
		{
			get
			{
				return this.nextUndo >= 0 && this.nextUndo <= this.historyList.Count - 1;
			}
		}

		public bool CanRedo
		{
			get
			{
				return this.nextUndo != this.historyList.Count - 1;
			}
		}

		public void ClearHistory()
		{
			this.historyList = new List<Command>();
			this.nextUndo = -1;
		}

		public void AddCommandToHistory(Command command)
		{
			this.TrimHistoryList();
			this.historyList.Add(command);
			this.nextUndo++;
		}

		public void RemoveCommandFromHistory(Command command)
		{
			this.historyList.Remove(command);
			this.nextUndo--;
		}

		public void RenameLayer(string oldName, string newName)
		{
			foreach (Command command in this.historyList)
			{
				command.RenameLayer(oldName, newName);
			}
		}

		public void Undo()
		{
			if (!this.CanUndo)
			{
				return;
			}
			Command command = this.historyList[this.nextUndo];
			command.Undo();
			this.nextUndo--;
		}

		public void Redo()
		{
			if (!this.CanRedo)
			{
				return;
			}
			int index = this.nextUndo + 1;
			Command command = this.historyList[index];
			command.Redo();
			this.nextUndo++;
		}

		private void TrimHistoryList()
		{
			if (this.historyList.Count == 0)
			{
				return;
			}
			if (this.nextUndo == this.historyList.Count - 1)
			{
				return;
			}
			Console.WriteLine("Purge all items below the NextUndo pointer");
			for (int i = this.historyList.Count - 1; i > this.nextUndo; i--)
			{
				Console.WriteLine(string.Format("Purge item {0} below the NextUndo pointer", i));
				this.historyList.RemoveAt(i);
			}
		}

		private List<Command> historyList;

		private int nextUndo;
	}
}
