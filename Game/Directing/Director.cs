using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System;

namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            

            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);
            // Console.WriteLine(robot.GetVelocity().GetX());
            
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            // this is where I should move the artifacts down the screen
            
            
            // Console.WriteLine(newArtifact.GetVelocity().GetX());
            // Console.WriteLine();
            // Console.WriteLine("bleh");
            // Console.WriteLine(newArtifact);

            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> artifacts = cast.GetActors("artifacts");

            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();

            Random random = new Random();
            // Actor newArtifact = cast.GetFirstActor("artifacts");
            foreach (Actor artifact in artifacts)
            {
                Point artVelocity = new Point(0, 5);
                artifact.SetVelocity(artVelocity);
                

                int index = random.Next(0, artifacts.Count);
                artifacts[index].MoveNext(maxX, maxY);

                // foreach (Actor art in artifacts)
                // {
                //     art.MoveNext(maxX, maxY);
                // }

            }
            
            // move the artifacts
            // call method from class

            // for (int i = 0; i < 10; i++)
            // {

            // }

            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in artifacts)
            {
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    // this one can be used to RemoveActor...yeah or maybe put it in the do updates?
                    Artifact artifact = (Artifact)actor;
                    // string message = artifact.GetMessage();
                    // banner.SetText(message);
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();

            
            
        }



    }
}
