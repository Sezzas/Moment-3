using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

namespace guestbook
{
    public class GuestBook {

        private string file = @"guestbook.json"; // JSON-file
        private List<Post> posts = new List<Post>(); // List with posts

        // Read file
        public GuestBook() {
            if(File.Exists(@"guestbook.json")==true) {
                string jsonString = File.ReadAllText(file);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }

        // Get posts
        public List<Post> getPosts() {
            return posts;
        }

        // Add new post
        public Post addPost(Post post) {
            posts.Add(post);
            marshal();
            return post;
        }

        // Delete post
        public int deletePost(int index) {
            posts.RemoveAt(index);
            marshal();
            return index;
        }

        // Serialize posts
        private void marshal() {
            var jsonString = JsonSerializer.Serialize(posts);
            File.WriteAllText(file, jsonString);
        }
    }

    
    public class Post {

        private string author;
        public string Author 
        {
            set {this.author = value;}
            get {return this.author;}
        }
        private string message;
        public string Message
        {
            set {this.message = value;}
            get {return this.message;}
        }
    }


    class Program {

        static void Main(string[] args) {

            GuestBook guestbook = new GuestBook();
            int i = 0;

            while(true) {
                Console.Clear(); Console.CursorVisible = false;
                // Print menu
                Console.WriteLine("E B B A S  GÄSTBOK\n\n");
                Console.WriteLine("1. Skriv i gästboken");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n\n");
                Console.WriteLine("Nuvarande inlägg:");

                // Loop through posts
                i = 0;
                foreach(Post post in guestbook.getPosts()) {
                    Console.WriteLine("[" + i++ + "] " + post.Author + " - \"" + post.Message + "\"");
                }

                // Listen for key inputs
                int input = (int) Console.ReadKey(true).Key;
                switch(input) {
                    case '1': // Press 1
                        Console.CursorVisible = true;
                        Console.Write("Ange ditt namn: ");
                        string author = Console.ReadLine();
                        Console.Write("Skriv ditt meddelande: ");
                        string message = Console.ReadLine();
                        if(String.IsNullOrEmpty(author) || String.IsNullOrEmpty(message)) {
                            break;
                        } else {
                            // Create new post
                            Post obj = new Post();
                            obj.Author = author;
                            obj.Message = message;
                            guestbook.addPost(obj);
                            break;
                        }
                    case '2': // Press 2
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        if(String.IsNullOrEmpty(index)) {
                            break;
                        } else {
                            // Delete post
                            guestbook.deletePost(Convert.ToInt32(index));
                            break;
                        }
                    case 88: // Press X
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }
}