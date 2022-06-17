using HotChocolate.Execution.Configuration;

namespace CloudIn.Contexts.Files.GraphQL;

public static class FilesTypeExtensions
{
    public static IRequestExecutorBuilder AddFilesTypeExtension(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddTypeExtension<FilesQuery>()
            .AddTypeExtension<FilesMutation>();
    }
}