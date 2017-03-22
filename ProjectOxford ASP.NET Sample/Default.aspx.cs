using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Emotion;

namespace ProjectOxford_ASP.NET_Sample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void LoadButton_Click(Object sender, EventArgs e)
        {
            if (this.fileUploader.HasFile)
            {
                this.fileUploader.SaveAs("C:\\Users\\%username%\\Desktop" + this.fileUploader.FileName);
                try
                {
                    string imageFile = fileUploader.ResolveUrl(this.fileUploader.FileName);
                    string absolutePath = Path.GetFullPath(imageFile);

                    imgBox.Height = 300;
                    imgBox.Width = 200;
                    imgBox.ImageUrl = absolutePath;


                    FaceDetection(absolutePath);
                }
                catch (Exception ex)
                {
                    Console.Write("Error: " + ex.Message);
                }
            }
        }




        public async Task FaceDetection(string pathToFile)
        {
            string subscriptionKeyFace = "insert Face API Key here";
            string subscriptionKeyEmo = "insert Emotions API Key here";


            FaceServiceClient fSC = new FaceServiceClient(subscriptionKeyFace);

            EmotionServiceClient eSC = new EmotionServiceClient(subscriptionKeyEmo);

            using (Stream s = File.OpenRead(pathToFile))
            {
                var requiredFaceAttributes = new FaceAttributeType[] {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Smile,
                FaceAttributeType.FacialHair,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Glasses
                };

                var faces = await fSC.DetectAsync(s,
                                                  returnFaceLandmarks: true,
                                                  returnFaceAttributes: requiredFaceAttributes);


                numOfFacesLabel.Text = "Number of Faces:" + faces.Length;

                var faceRectangles = new List<Microsoft.ProjectOxford.Common.Rectangle>();

                foreach (var face in faces)
                {
                    var rect = face.FaceRectangle;

                    var landmarks = face.FaceLandmarks;

                    var age = face.FaceAttributes.Age;
                    var gender = face.FaceAttributes.Gender;
                    var glasses = face.FaceAttributes.Glasses;


                    var rectangle = new Microsoft.ProjectOxford.Common.Rectangle
                    {
                        Height = face.FaceRectangle.Height,
                        Width = face.FaceRectangle.Width,
                        Top = face.FaceRectangle.Top,
                        Left = face.FaceRectangle.Left,
                    };
                    faceRectangles.Add(rectangle);


                    try
                    {
                        ageLabel.Text = "Age: " + age;
                        Console.Write("Age: " + age);

                        genderLabel.Text = "Gender: " + gender;
                        glassesLabel.Text = "Does the person wear glasses? " + glasses;
                    }
                    catch (Exception e)
                    {
                        Console.Write("Error: " + e);
                    }


                    if (faces.Length > 0)
                    {


                        using (Stream str = File.OpenRead(pathToFile))
                        {
                            var emotions = await eSC.RecognizeAsync(str, faceRectangles.ToArray());


                            string emotionsList = "";

                            foreach (var emotion in emotions)
                            {

                                emotionsList += $@"Anger: {emotion.Scores.Anger}
                                    Contempt: {emotion.Scores.Contempt}
                                    Disgust: {emotion.Scores.Disgust}
                                    Fear: {emotion.Scores.Fear}
                                    Happiness: {emotion.Scores.Happiness}
                                    Neutral: {emotion.Scores.Neutral}
                                    Sadness: {emotion.Scores.Sadness}                                    
                                    Surprise: {emotion.Scores.Surprise}";
                            }

                            emotionsLabel.Text = emotionsList;

                        }
                    }
                }
            }
        }
    }
}