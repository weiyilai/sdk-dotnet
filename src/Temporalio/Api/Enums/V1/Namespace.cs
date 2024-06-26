// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: temporal/api/enums/v1/namespace.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Temporalio.Api.Enums.V1 {

  /// <summary>Holder for reflection information generated from temporal/api/enums/v1/namespace.proto</summary>
  public static partial class NamespaceReflection {

    #region Descriptor
    /// <summary>File descriptor for temporal/api/enums/v1/namespace.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static NamespaceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiV0ZW1wb3JhbC9hcGkvZW51bXMvdjEvbmFtZXNwYWNlLnByb3RvEhV0ZW1w",
            "b3JhbC5hcGkuZW51bXMudjEqjgEKDk5hbWVzcGFjZVN0YXRlEh8KG05BTUVT",
            "UEFDRV9TVEFURV9VTlNQRUNJRklFRBAAEh4KGk5BTUVTUEFDRV9TVEFURV9S",
            "RUdJU1RFUkVEEAESHgoaTkFNRVNQQUNFX1NUQVRFX0RFUFJFQ0FURUQQAhIb",
            "ChdOQU1FU1BBQ0VfU1RBVEVfREVMRVRFRBADKmgKDUFyY2hpdmFsU3RhdGUS",
            "HgoaQVJDSElWQUxfU1RBVEVfVU5TUEVDSUZJRUQQABIbChdBUkNISVZBTF9T",
            "VEFURV9ESVNBQkxFRBABEhoKFkFSQ0hJVkFMX1NUQVRFX0VOQUJMRUQQAipz",
            "ChBSZXBsaWNhdGlvblN0YXRlEiEKHVJFUExJQ0FUSU9OX1NUQVRFX1VOU1BF",
            "Q0lGSUVEEAASHAoYUkVQTElDQVRJT05fU1RBVEVfTk9STUFMEAESHgoaUkVQ",
            "TElDQVRJT05fU1RBVEVfSEFORE9WRVIQAkKGAQoYaW8udGVtcG9yYWwuYXBp",
            "LmVudW1zLnYxQg5OYW1lc3BhY2VQcm90b1ABWiFnby50ZW1wb3JhbC5pby9h",
            "cGkvZW51bXMvdjE7ZW51bXOqAhdUZW1wb3JhbGlvLkFwaS5FbnVtcy5WMeoC",
            "GlRlbXBvcmFsaW86OkFwaTo6RW51bXM6OlYxYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Temporalio.Api.Enums.V1.NamespaceState), typeof(global::Temporalio.Api.Enums.V1.ArchivalState), typeof(global::Temporalio.Api.Enums.V1.ReplicationState), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum NamespaceState {
    [pbr::OriginalName("NAMESPACE_STATE_UNSPECIFIED")] Unspecified = 0,
    [pbr::OriginalName("NAMESPACE_STATE_REGISTERED")] Registered = 1,
    [pbr::OriginalName("NAMESPACE_STATE_DEPRECATED")] Deprecated = 2,
    [pbr::OriginalName("NAMESPACE_STATE_DELETED")] Deleted = 3,
  }

  public enum ArchivalState {
    [pbr::OriginalName("ARCHIVAL_STATE_UNSPECIFIED")] Unspecified = 0,
    [pbr::OriginalName("ARCHIVAL_STATE_DISABLED")] Disabled = 1,
    [pbr::OriginalName("ARCHIVAL_STATE_ENABLED")] Enabled = 2,
  }

  public enum ReplicationState {
    [pbr::OriginalName("REPLICATION_STATE_UNSPECIFIED")] Unspecified = 0,
    [pbr::OriginalName("REPLICATION_STATE_NORMAL")] Normal = 1,
    [pbr::OriginalName("REPLICATION_STATE_HANDOVER")] Handover = 2,
  }

  #endregion

}

#endregion Designer generated code
