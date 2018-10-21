namespace YoureAProgrammer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.Models;
    using Services;
    using Xamarin.Forms;

    public class QuestionsViewModel: BaseViewModel
    {
        private ObservableCollection<Questions> questions;
        private ApiServices apiService;
        public ObservableCollection<Questions> Questions
        {
            get { return this.questions; }
            set { this.SetValue(ref this.questions, value); }
        }

        public QuestionsViewModel()
        {
            apiService = new ApiServices();
            this.LoadQuestions();
        }

        private async void LoadQuestions()
        {
            var response = await this.apiService.GetList<Questions>("http://localhost:61927", "/api", "/Questions");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message, "Aceptar");
                return;
            }

            var list = (List<Questions>)response.Result;
            this.Questions = new ObservableCollection<Questions>(list);
        }
    }
}
