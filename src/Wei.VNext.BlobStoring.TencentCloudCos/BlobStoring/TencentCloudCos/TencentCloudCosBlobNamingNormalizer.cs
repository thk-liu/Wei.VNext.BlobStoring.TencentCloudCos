﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace VNext.BlobStoring.TencentCloudCos
{
    public class TencentCloudCosBlobNamingNormalizer : IBlobNamingNormalizer, ITransientDependency
    {
        public virtual string NormalizeBlobName(string blobName)
        {
            return blobName;
        }

        public virtual string NormalizeContainerName(string containerName)
        {
            // All letters in a container name must be lowercase.
            containerName = containerName.ToLower();

            // Container names can contain only letters, numbers, and the dash (-) character.
            containerName = Regex.Replace(containerName, "[^a-z0-9-]", string.Empty);

            // Every dash (-) character must be immediately preceded and followed by a letter or number;
            // consecutive dashes are not permitted in container names.
            // Container names must start or end with a letter or number
            containerName = Regex.Replace(containerName, "-{2,}", "-");
            containerName = Regex.Replace(containerName, "^-", string.Empty);
            containerName = Regex.Replace(containerName, "-$", string.Empty);

            // Container names must be from 3 through 63 characters long.
            if (containerName.Length < 3)
            {
                var length = containerName.Length;
                for (var i = 0; i < 3 - length; i++)
                {
                    containerName += "0";
                }
            }

            if (containerName.Length > 63)
            {
                containerName = containerName.Substring(0, 63);
            }

            return containerName;
        }
    }
}
