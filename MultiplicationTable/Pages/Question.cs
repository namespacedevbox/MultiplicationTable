namespace MultiplicationTable.Pages
{
    public class Questions
    {
        public string Question { get; set; }
        public int? Answer { get; set; }
        public bool IsCorrect => Answer == CorrectAnswer;

        private int CorrectAnswer { get; }

        public Questions(int x, int y)
        {
            Question = $"{x} x {y} =";
            CorrectAnswer = x * y;
        }
    }
}
