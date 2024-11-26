using MediatR;

namespace Application.Command.TransferFileIntoModel
{
    public sealed record TransformFileCommand(string FilePath) : IRequest<TransformFileCommandResponse>;
}
