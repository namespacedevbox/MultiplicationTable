using Microsoft.AspNetCore.Components.Web;

namespace MultiplicationTable.Pages
{
    public partial class Index
    {
        private List<Questions> questions;
        private bool showResults;
        private Questions currentQuestion;

        protected override void OnInitialized()
        {
           

            questions = new List<Questions>();
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 2; y <= 10; y++)
                {
                    questions.Add(new Questions(x, y));
                }
            }
            questions.Shuffle();
            NextQuestion();
        }

        private void NextQuestion()
        {
            currentQuestion = questions.FirstOrDefault(q => q.Answer == null);
            if (currentQuestion == null)
            {
                showResults = true;
            }
        }

        private void Skip()
        {
            currentQuestion.Answer = 0;
            NextQuestion();
        }

        private void CalcResult()
        {
            var ok = questions.Where(x=>x.IsCorrect == true).ToList();
            var wrong = questions.Where(x=>x.IsCorrect == false).ToList();

        }

        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                NextQuestion();
            }
        }
    }
}