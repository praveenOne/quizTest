public struct song
{
    public string id;
    public string title;
    public string artist;
    public string picture;
    public string sample;
}

public struct choices
{
    public string artist;
    public string title;
}

public struct questions
{
    public string id;
    public int answerIndex;
    public choices[] choices;
    public song song;
}

public struct quiz
{
    public string id;
    public questions[] questions;
    public string playlist;
}

public struct userChoice
{
    public int questionIndex;
    public int userFeedback;

    public userChoice(int index, int feedback)
    {
        this.questionIndex = index;
        this.userFeedback = feedback;
    }
}

