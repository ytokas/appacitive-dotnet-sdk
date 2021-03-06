﻿using Appacitive.Sdk.Internal;
using Appacitive.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Appacitive.Sdk
{
    /// <summary>
    /// Helper class which contains static lookup methods for APDevice objects.
    /// </summary>
    public static class APDevices
    {

        /// <summary>
        /// Finds a paginated list of APDevices for the given type and search criteria.
        /// </summary>
        /// <param name="query">The search query</param>
        /// <param name="fields">The device specific fields to be retrieved.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="orderBy">The field on which to sort.</param>
        /// <param name="sortOrder">Sort order - Ascending or Descending.</param>
        /// <param name="options">Request specific api options. These will override the global settings for the app for this request.</param>
        /// <returns>A paginated list of APDevice objects for the given search criteria.</returns>
        public async static Task<PagedList<APDevice>> FindAllAsync(IQuery query = null, IEnumerable<string> fields = null, int pageNumber = 1, int pageSize = 20, string orderBy = null, SortOrder sortOrder = SortOrder.Descending, ApiOptions options = null)
        {
            var objects = await APObjects.FindAllAsync("device", query, fields, pageNumber, pageSize, orderBy, sortOrder, options);
            var devices = objects.Select(x => x as APDevice);
            var list =  new PagedList<APDevice>()
            {
                PageNumber = objects.PageNumber,
                PageSize = objects.PageSize,
                TotalRecords = objects.TotalRecords,
                GetNextPage = async skip => await FindAllAsync(query, fields, pageNumber + skip + 1, pageSize, orderBy, sortOrder, options)
            };
            list.AddRange(devices);
            return list;
        }

        /// <summary>
        /// Delets the device with the given id.
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="options">Request specific api options. These will override the global settings for the app for this request.</param>
        public async static Task DeleteAsync(string id, ApiOptions options = null)
        {
            var request = new DeleteDeviceRequest() { Id = id };
            ApiOptions.Apply(request, options);
            var response = await request.ExecuteAsync();
            if (response.Status.IsSuccessful == false)
                throw response.Status.ToFault();
        }


        /// <summary>
        /// Gets an existing APDevice object by its id.
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="fields">The device object fields to be retrieved.</param>
        /// <returns>The APDevice object with the given id.</returns>
        public async static Task<APDevice> GetAsync(string id, IEnumerable<string> fields = null, ApiOptions options = null)
        {
            var request = new GetDeviceRequest() { Id = id};
            if (fields != null)
                request.Fields.AddRange(fields);
            ApiOptions.Apply(request, options);
            var response = await request.ExecuteAsync();
            if (response.Status.IsSuccessful == false)
                throw response.Status.ToFault();
            Debug.Assert(response.Device != null, "For a successful get call, device should always be returned.");
            return response.Device;
        }

        
    }
}
