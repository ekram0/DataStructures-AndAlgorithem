using ContactManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.Commands
{
    internal class AddCommand : Command
    {
        public AddCommand(ContactStore store, IReadOnlyDictionary<string, string> args)
            : base(Commands.Add, args, store)
        {
        }

        public override CommandResult Execute => new AddCommandResult(Store, this,
                new List<Contact> { Store.Add(CommandArgParser.ContactFromArgs(Args)) });

        private class AddCommandResult : CommandResult
        {
            readonly ContactStore store;

            public AddCommandResult(ContactStore store, ICommand command, IEnumerable<Contact> contacts)
                : base(command, contacts) => this.store = store;

            public override bool CanUndo => true;

            public override ICommand UndoCommand => new DirectRemoveCommand(store, Results);
        }
    }

    internal class DirectAddCommand : ICommand
    {
        private readonly ContactStore store;
        private readonly IEnumerable<Contact> contacts;

        public DirectAddCommand(
            ContactStore store, IEnumerable<Contact> contacts)
        {
            this.store = store;
            this.contacts = contacts;
        }

        public string Verb => Commands.Add;

        public IReadOnlyDictionary<string, string> Args => new Dictionary<string, string>(0);

        public CommandResult Execute => new NonUndoCommandResult(this, store.Add(contacts));
    }
}
