using System.Collections.Generic;
using System.Linq;
using ELFinder.Connector.Drivers.Common.Data.Volumes;
using ELFinder.Connector.Exceptions;

namespace ELFinder.Connector.Drivers.Common.Data.Extensions
{

    /// <summary>
    /// Volume extensions
    /// </summary>
    public static class VolumeExtensions
    {

        #region Extension methods

        /// <summary>
        /// Get volume
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volumes">Volumes list</param>
        /// <param name="volumeId">Volume id</param>
        /// <returns>Result root volume</returns>
        public static TVolume GetVolume<TVolume>(this IEnumerable<TVolume> volumes, string volumeId)
            where TVolume : RootVolume
        {

            // Normalize volumes list
            var normalizedVolumes = volumes as IList<TVolume> ?? volumes.ToList();

            // Validate volumes list
            if (!normalizedVolumes.Any()) throw new ELFinderNoVolumesDefinedException();

            // Get volume
            var volume = normalizedVolumes.FirstOrDefault(x => x.VolumeId == volumeId);

            // Validate it was found
            if(volume == null) throw new ELFinderVolumeNotFoundException(volumeId);

            // Return it
            return volume;

        }

        #endregion

    }
}