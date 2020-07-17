﻿using ContactManager;
using ContactManager.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.Commands
{
    internal class RemoveCommand : Command
    {
        internal RemoveCommand(ContactStore store, IReadOnlyDictionary<string, string> args)
            : base(Commands.Remove, args, store)
        {
        }

        public override CommandResult Execute
        {
            get
            {
                ContactFieldFilter filter = CommandArgParser.FilterFromArgs(Args);
                if (filter.HasFilter)
                {
                    Contact removed;
                    if (Store.Remove(filter, out removed))
                    {
                        return new RemoveCommandResult(Store, this, new List<Contact>(1) { removed });
                    }
                }
                else
                {
                    Log.Warning("Remove requires at least one filter - skipping");
                }

                return new NonUndoCommandResult(this, Store.Search(CommandArgParser.FilterFromArgs(Args)));
            }
        }

        private class RemoveCommandResult : CommandResult
        {
            readonly ContactStore store;

            public RemoveCommandResult(ContactStore store, ICommand command, IEnumerable<Contact> contacts)
                : base(command, contacts)
            {
                this.store = store;
            }

            public override bool CanUndo
            {
                get
                {
                    return true;
                }
            }

            public override ICommand UndoCommand => new DirectAddCommand(store, Results);
        }
    }

    internal class DirectRemoveCommand : ICommand
    {
        private readonly ContactStore store;
        private readonly IEnumerable<Contact> contacts;

        public DirectRemoveCommand(ContactStore store, IEnumerable<Contact> contacts)
        {
            this.store = store;
            this.contacts = contacts;
        }

        public string Verb => Commands.Remove;

        public IReadOnlyDictionary<string, string> Args => new Dictionary<string, string>(0);

        public CommandResult Execute
        {
            get
            {
                List<Contact> removed = new List<Contact>();
                foreach (Contact c in contacts)
                {
                    if (store.Remove(c, out Contact r))
                    {
                        removed.Add(r);
                    }
                }

                return new NonUndoCommandResult(this, removed);
            }
        }
    }
}
