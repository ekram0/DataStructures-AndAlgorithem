using ContactManager;
using System.Collections.Generic;

namespace cm.Commands
{
    internal interface ICommand
    {
        string Verb { get; }
        IReadOnlyDictionary<string, string> Args { get; }

        CommandResult Execute { get; }
    }

    internal abstract class CommandResult
    {
        // The command that executed
        public readonly ICommand Command;
        
        // The results of the command
        public readonly IEnumerable<Contact> Results;

        public CommandResult(
            ICommand originalCommand, IEnumerable<Contact> contacts)
        {
            this.Command = originalCommand;
            this.Results = contacts;
        }

        // True if the command is undo-able
        public abstract bool CanUndo { get; }

        // Gets the command to perform the Undo operation
        public abstract ICommand UndoCommand { get; }
    }

    internal class NonUndoCommandResult : CommandResult
    {
        public NonUndoCommandResult(ICommand command, IEnumerable<Contact> contacts)
            : base(command, contacts)
        {
        }

        public override bool CanUndo => false;

        public override ICommand UndoCommand => new NoopCommand();
    }

}

namespace cm.Commands
{
    internal abstract class Command : ICommand
    {
        public string Verb { get; }

        public IReadOnlyDictionary<string, string> Args { get; }

        protected ContactStore Store { get; }

        protected Command(string verb, IReadOnlyDictionary<string, string> args, ContactStore store)
        {
            Verb = verb;
            Args = args;
            Store = store;
        }

        public abstract CommandResult Execute { get; }
    }
}

namespace cm.Commands
{
    internal struct Commands
    {
        public const string Add = "add";
        public const string Remove = "remove";
        public const string Find = "find";
        public const string Quit = "quit";
        public const string Error = "error";
        public const string List = "list";
        public const string Load = "load";
        public const string Save = "save";
        public const string Dump = "dump";
        public const string Undo = "undo";
    }
}