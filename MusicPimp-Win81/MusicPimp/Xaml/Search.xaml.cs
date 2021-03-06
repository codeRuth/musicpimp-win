﻿using Mle.Messaging;
using Mle.MusicPimp.Messaging;
using Mle.MusicPimp.ViewModels;
using Mle.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace Mle.MusicPimp.Xaml {
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class Search : BasePage {

        public StoreSearch SearchModel { get; private set; }

        public Search() {
            this.InitializeComponent();
            SearchModel = new StoreSearch();
            // datacontext set in xaml
            DataContext = SearchModel;
            //Loaded += (s, e) => NavigateToRememberedPosition();
        }
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session. This will be null the first time a page is visited.</param>
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState) {
            var term = navigationParameter as string;
            await SearchModel.Search(term);
            SearchField.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
        //private void NavigateToRememberedPosition() {
        //    var item = Model.CurrentScrollPosition();
        //    if(item != null) {
        //        // Might throw? Not sure, but I suppress anyway as this is merely convenience if it works.
        //        Utils.Suppress<Exception>(() => itemGridView.ScrollIntoView(item, ScrollIntoViewAlignment.Leading));
        //    }
        //}

        private void OnGridTapped(object sender, TappedRoutedEventArgs e) {
            SearchModel.Actions.ClearSelection();
        }

        private void OnListTapped(object sender, TappedRoutedEventArgs e) {
            SearchModel.Actions.ClearSelection();
        }

        private void HelpItemClicked(object sender, ItemClickEventArgs e) {
            var item = (TitledImageItem)e.ClickedItem;
            item.OnClicked();
        }

        private void SearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args) {
            var query = args.QueryText;
            PageNavigationService.Instance.NavigateWithParam(PageNames.SEARCH, query);
        }

    }
}
