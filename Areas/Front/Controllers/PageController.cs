﻿using System.Threading.Tasks;
using Bonsai.Areas.Front.Logic;
using Bonsai.Code.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Areas.Front.Controllers
{
    /// <summary>
    /// The root controller for pages.
    /// </summary>
    [Area("Front")]
    [Route("p")]
    public class PageController: Controller
    {
        public PageController(PageService pages)
        {
            _pages = pages;
        }

        private readonly PageService _pages;

        /// <summary>
        /// Displays the page description.
        /// </summary>
        [Route("{key}")]
        public async Task<ActionResult> Description(string key)
        {
            var encKey = PageHelper.EncodeTitle(key);
            if (encKey != key)
                return RedirectToActionPermanent("Description", new {key = encKey});

            var vm = await _pages.GetPageDescriptionAsync(encKey)
                                 .ConfigureAwait(false);
            return View(vm);
        }

        /// <summary>
        /// Displays the facts information.
        /// </summary>
        [Route("{key}/facts")]
        public async Task<ActionResult> Facts(string key)
        {
            var encKey = PageHelper.EncodeTitle(key);
            if (encKey != key)
                return RedirectToActionPermanent("Facts", new { key = encKey });

            var vm = await _pages.GetPageFactsAsync(encKey)
                                 .ConfigureAwait(false);
            return View(vm);
        }


        /// <summary>
        /// Displays the related media files.
        /// </summary>
        [Route("{key}/media")]
        public async Task<ActionResult> Media(string key)
        {
            var encKey = PageHelper.EncodeTitle(key);
            if (encKey != key)
                return RedirectToActionPermanent("Media", new { key = encKey });

            var vm = await _pages.GetPageMediaAsync(encKey)
                                 .ConfigureAwait(false);
            return View(vm);
        }
    }
}
