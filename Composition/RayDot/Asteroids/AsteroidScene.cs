using System; // Random, Math
using System.Numerics; // Vector2
using System.Collections.Generic; // List
using RayDot; // RayDot
using Raylib_cs; // Raylib

namespace UserLand
{
	class AsteroidScene : SceneNode
	{
		private SpaceShip player;
		private List<Bullet> bullets;
		private List<Asteroid> asteroids;
		private Node victory;
		private Node gameover;
		private Node fpstext;
		private float timer;
		private int framecounter;

		public AsteroidScene() : base()
		{
			Reload();
		}

		public void Reload()
		{
			ClearChildren();
			State = State.Playing;

			victory = new Node();
			victory.AddComponent("Sprite", new SpriteComponent(victory, "resources/victory.png"));
			victory.Sprite.Color = Color.GREEN;
			victory.Transform.Position = new Vector2(0, -256); // outside screen
			AddChild(victory);

			gameover = new Node();
			gameover.AddComponent("Sprite", new SpriteComponent(gameover, "resources/gameover.png"));
			gameover.Sprite.Color = Color.RED;
			gameover.Transform.Position = new Vector2(0, -256); // outside screen
			AddChild(gameover);

			player = new SpaceShip();
			AddChild(player);

			fpstext = new Node();
			fpstext.AddComponent("Text", new TextComponent(fpstext, "calculating FPS...", 20));
			fpstext.Transform.Position = new Vector2(10, 10);
			fpstext.Text.Color = Color.GREEN;
			AddChild(fpstext);
			timer = 0.0f;
			framecounter = 0;

			bullets = new List<Bullet>();
			asteroids = new List<Asteroid>();

			SpawnAsteroids(5, Settings.ScreenSize / 2, Generation.First);
		}

		public override void Update(float deltaTime)
		{
			// Calculate framerate
			framecounter++;
			timer += deltaTime;
			if (timer > 1.0f) {
				fpstext.Text.PrintText = framecounter.ToString() + " FPS";
				framecounter = 0;
				timer = 0.0f;
			}

			HandleInput(deltaTime);

			HandlePlayer(deltaTime);
			HandleBullets(deltaTime);
			HandleAsteroids(deltaTime);

			if (asteroids.Count == 0)
			{
				State = State.Won;
			}
			if (State == State.Won)
			{
				victory.Transform.Position = Settings.ScreenSize / 2;
			}
			if (State == State.Lost)
			{
				gameover.Transform.Position = Settings.ScreenSize / 2;
			}
		}

		private void HandlePlayer(float deltaTime)
		{
			if (State == State.Lost)
			{
				return;
			}
			// player <-> asteroids
			foreach (Asteroid asteroid in asteroids)
			{
				// distance to player
				float distance = Vector2.Distance(asteroid.Transform.WorldPosition, player.Transform.WorldPosition);
				float toCheck = player.Body.Sprite.TextureSize.X / 2 + (asteroid.Sprite.TextureSize.X / 2 * asteroid.Transform.Scale.X) * 0.8f;

				if (distance < toCheck)
				{
					if (HasChild(player)) {
						RemoveChild(player);
					}
					State = State.Lost;
				}
			}
		}

		private void HandleAsteroids(float deltaTime)
		{
			if (State == State.Lost)
			{
				return;
			}

			// asteroids <-> bullets
			List<Bullet> bulletsToDelete = new List<Bullet>();
			List<Asteroid> asteroidsToDelete = new List<Asteroid>();
			List<Vector2> positionsToSpawnSecondGen = new List<Vector2>();
			List<Vector2> positionsToSpawnThirdGen = new List<Vector2>();
			foreach (Asteroid asteroid in asteroids)
			{
				foreach (Bullet bullet in bullets)
				{
					// distance to bullet
					float distance = Vector2.Distance(asteroid.Transform.WorldPosition, bullet.Transform.WorldPosition);
					float toCheck = asteroid.Sprite.TextureSize.X / 2 * asteroid.Transform.Scale.X;
					if (distance < toCheck)
					{
						if (asteroid.Generation == Generation.First)
						{
							asteroidsToDelete.Add(asteroid);
							bulletsToDelete.Add(bullet);
							positionsToSpawnSecondGen.Add(asteroid.Transform.WorldPosition);
						}
						if (asteroid.Generation == Generation.Second)
						{
							asteroidsToDelete.Add(asteroid);
							bulletsToDelete.Add(bullet);
							positionsToSpawnThirdGen.Add(asteroid.Transform.WorldPosition);
						}
						if (asteroid.Generation == Generation.Third)
						{
							asteroidsToDelete.Add(asteroid);
							bulletsToDelete.Add(bullet);
						}
					}
				}
			}

			// Spawn new Asteroids
			foreach (var pos in positionsToSpawnSecondGen)
			{
				SpawnAsteroids(2, pos, Generation.Second);
			}
			foreach (var pos in positionsToSpawnThirdGen)
			{
				SpawnAsteroids(3, pos, Generation.Third);
			}

			// delete Asteroids and Bullets
			foreach (Asteroid asteroid in asteroidsToDelete)
			{
				asteroids.Remove(asteroid);
				RemoveChild(asteroid);
			}
			foreach (Bullet bullet in bulletsToDelete)
			{
				bullets.Remove(bullet);
				RemoveChild(bullet);
			}

		}

		private void SpawnAsteroids(int amount, Vector2 pos, Generation gen)
		{
			var rand = new Random();
			for (int i = 0; i < amount; i++)
			{
				Asteroid a = new Asteroid(gen);
				a.Transform.Position = pos;
				if (gen == Generation.Second)
				{
					a.Transform.Scale = new Vector2(0.7f, 0.7f);
				}
				if (gen == Generation.Third)
				{
					a.Transform.Scale = new Vector2(0.3f, 0.3f);
				}

				Vector2 vel = new Vector2();
				vel.X = (float) (rand.NextDouble() * 400) - 200;
				vel.Y = (float) (rand.NextDouble() * 400) - 200;
				a.Mover.Velocity = vel;

				float pi = (float) Math.PI;
				a.RotSpeed = (float) (rand.NextDouble() * pi * 4) - pi * 2;

				asteroids.Add(a);
				AddChild(a);
			}
		}

		private void HandleBullets(float deltaTime)
		{
			// keep a list of bullets we might want to delete
			List<Bullet> toDelete = new List<Bullet>();

			// update all bullets
			foreach (Bullet bullet in bullets)
			{
				// check if bullet is outside screen area
				if (bullet.Transform.Position.Y < 0 ||
					bullet.Transform.Position.Y > Settings.ScreenSize.Y ||
					bullet.Transform.Position.X < 0 ||
					bullet.Transform.Position.X > Settings.ScreenSize.X
					)
				{
					toDelete.Add(bullet);
				}
			}

			// delete bullets from bulletlist and scenegraph
			foreach (Bullet bullet in toDelete)
			{
				bullets.Remove(bullet);
				RemoveChild(bullet);
			}
			toDelete.Clear();
		}

		private void HandleInput(float deltaTime)
		{
			// Reload Game
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_R))
			{
				Reload();
			}
			// Player Rotate
			if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
			{
				player.RotateRight(deltaTime);
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
			{
				player.RotateLeft(deltaTime);
			}
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT))
			{
				player.StopRotating();
			}
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT))
			{
				player.StopRotating();
			}
			// Player Thrust
			if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
			{
				player.Thrust();
			}
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_UP))
			{
				player.NoThrust();
			}
			// Player Shoot
			if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
			{
				if (!(State == State.Lost))
				{
					Bullet bullet = player.Shoot(deltaTime);
					if (bullet != null)
					{
						AddChild(bullet);
						bullets.Add(bullet);
					}
				}
			}

			// Camera
			/*
			float camspeed = 200.0f;
			if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
			{
				Camera.X += deltaTime * camspeed;
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
			{
				Camera.X -= deltaTime * camspeed;
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
			{
				Camera.Y += deltaTime * camspeed;
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
			{
				Camera.Y -= deltaTime * camspeed;
			}
			*/
		}
	}
}
