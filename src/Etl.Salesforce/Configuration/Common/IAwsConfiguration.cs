namespace Eol.Cig.Etl.Salesforce.Configuration.Common
{
    public interface IAwsConfiguration
    {
        string AwsAccessKeyId { get; }
        string AwsSecretAccessKey { get; }
        string AwsKinesisStreamName { get; }
        string S3Prefix { get; }
        bool IsStreamingEnabled { get; }
    }
}
