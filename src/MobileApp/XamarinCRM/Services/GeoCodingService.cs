
using System;
using XamarinCRM.Services;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin;
using System.Collections.Generic;

[assembly: Dependency(typeof(GeoCodingService))]

namespace XamarinCRM.Services
{
    public class GeoCodingService : IGeoCodingService
    {
        readonly Geocoder _GeoCoder;

        public GeoCodingService()
        {
            _GeoCoder = new Geocoder();
        }

        #region IGeoCodingService implementation

        public Position NullPosition
        {
            get { return new Position(0, 0); }
        }

        public async Task<Position> GeoCodeAddress(string addressString)
        {
            Position p = NullPosition;

            try
            {
                p = (await _GeoCoder.GetPositionsForAddressAsync(addressString)).FirstOrDefault();

            }
            catch (Exception ex)
            {
                // TODO: log error
            }

            return p;
        }

        public async Task<string> GeoCodeGetFirstAddress(Position position)
        {
            string r = null;

            try
            {
                r = (await _GeoCoder.GetAddressesForPositionAsync(position)).FirstOrDefault();

            }
            catch (Exception ex)
            {
                // TODO: log error
            }

            return r;
        }

        public async Task<IEnumerable<string>> GeoCodeGetAddress(Position position)
        {
            IEnumerable<string> r = null;

            try
            {
                r = (await _GeoCoder.GetAddressesForPositionAsync(position));

            }
            catch (Exception ex)
            {
                // TODO: log error
            }

            return r;
        }

        #endregion
    }
}

