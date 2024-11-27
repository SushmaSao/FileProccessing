using Application.Contracts;
using MediatR;

namespace Application.Command.TransferFileIntoModel
{
    public sealed class TransformFileCommandHandler : IRequestHandler<TransformFileCommand, TransformFileCommandResponse>
    {
        private readonly IFileService _fileService;
        private readonly IFileMapper<InventoryItemDTO> _fileMapper;
        public TransformFileCommandHandler(IFileService fileService, IFileMapper<InventoryItemDTO> fileMapper)
        {
            _fileService = fileService;
            _fileMapper = fileMapper;
        }

        public async Task<TransformFileCommandResponse> Handle(TransformFileCommand request, CancellationToken cancellationToken)
        {
            TransformFileCommandResponse response = new TransformFileCommandResponse();

            //Skip the header line
            //   await _fileService.ReadFileLinesAsync(request.FilePath);

            //Read file line by line
            int i = 0;
            await foreach (var line in _fileService.ReadFileLinesAsync(request.FilePath, cancellationToken))
            {
                if (i != 0)
                {
                    var result = _fileMapper.MapContent(0, line);
                    if (result.ValidRecords != null)
                        response.ValidRecords.Add(result.ValidRecords);

                    if (result.InValidRecords != null)
                        response.InValidRecords.Add(result.InValidRecords);
                }
                i++;
            }

            return response;
        }
    }

}
