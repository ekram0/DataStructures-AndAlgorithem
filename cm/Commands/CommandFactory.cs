using ContactManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.Commands
{
    internal class CommandFactory
    {
        readonly ContactStore store;

        public CommandFactory(ContactStore store) => this.store = store;

        public ICommand Add(IReadOnlyDictionary<string, string> args) => new AddCommand(store, args);

        public ICommand Remove(IReadOnlyDictionary<string, string> args) => new RemoveCommand(store, args);

        public ICommand Find(IReadOnlyDictionary<string, string> args) => new FindCommand(store, args);

        public ICommand Quit() => new QuitCommand();

        public ICommand SyntaxError() => new SyntaxErrorCommand();

        public ICommand UnknownCommand(string command) => new UnknownCommand(command);


        public ICommand List(IReadOnlyDictionary<string, string> args) => new ListCommand(store, args);

        public ICommand Save(IReadOnlyDictionary<string, string> args) => new SaveCommand(store, args);

        public ICommand Load(IReadOnlyDictionary<string, string> args) => new LoadCommand(store, args);

        public ICommand Dump(IReadOnlyDictionary<string, string> args) => new DumpCommand();

        public ICommand Undo(IReadOnlyDictionary<string, string> args) => new UndoCommand();

    }
}
