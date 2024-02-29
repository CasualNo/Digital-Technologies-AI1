using System;
public class Program
{
	public static void Main()
	{
		int seed;
		Console.WriteLine("Before we begin, please input a number.");
		bool seedcheck;
		Random rand = new Random();
		do {
			seedcheck = int.TryParse(Console.ReadLine(), out seed);
			if (seedcheck == true) {
				rand = new Random(seed);
			} else {
				Console.WriteLine("Error: not a number\nPlease input a number.");
			}
		} while(seedcheck == false);
		string[] moves = {"rock", "paper", "scissors"};
		string randmove;
		randmove = moves[rand.Next(0,2)];
		bool replay = false;
		bool exit = false;
		bool ginval = false;
		bool menu;
		string choice;
		string rps;
		Console.Clear();
		Console.WriteLine("Welcome, what game would you like to play?\n");
		while (exit == false) {
			menu = false;
			Console.WriteLine("Type 'nac' for naughts and crosses, 'rps' for rock, paper, scissors, 'mmd' for mastermind, or 'exit' to exit.");
			choice = Console.ReadLine();
			choice = choice.ToLower();
			if (choice == "exit") {
				Console.WriteLine("Thanks for playing!");
				exit = true;
			} else if (choice == "nac") {
				do {
					replay = false;
					menu = Nacboard();
					if (menu == false) {
						Console.WriteLine("\nTo play again, type 'play'. To move back to the menu, type anything else.");
						choice = Console.ReadLine();
						if (choice.ToLower() == "play") {
							replay = true;
						}
					}
				} while (replay == true);
				Console.WriteLine("\nAlright then, what game would you like to play?\n");
			} else if (choice == "rps") {
				do {
					Console.Write("\nOkay, ");
					do {
						ginval = false;
						Console.WriteLine("will you choose rock, paper, or scissors? You can also return to the menu by typing 'menu'.");
						rps = Console.ReadLine();
						rps = rps.ToLower();
						randmove = moves[rand.Next(0,2)];
						if (rps == "rock" || rps == "paper" || rps ==  "scissors") {
							Console.WriteLine("Your opponent chose " + randmove + "\n");
							string result = Outcome(rps, randmove, moves);
							Console.WriteLine(result);
						} else if (rps == "menu"){
							menu = true;
							replay = false;
						} else {
							Console.WriteLine("\nSorry, that isn't a valid move.\n");
							ginval = true;
						}
					} while (ginval == true);
					if (menu == false) {
						Console.WriteLine("\nTo play again, type 'play'. To move back to the menu, type anything else.");
						choice = Console.ReadLine();
						if (choice.ToLower() == "play") {
							replay = true;
						} else {
							replay = false;
						}
					}
				} while (replay == true);
				Console.WriteLine("\nAlright then, what game would you like to play?\n");
			} else if (choice == "mmd") {
				do {
					replay = false;
					menu = Mind(rand);
					if (menu == false) {
						Console.WriteLine("\nTo play again, type 'play'. To move back to the menu, type anything else.");
						choice = Console.ReadLine();
						if (choice.ToLower() == "play") {
							replay = true;
						}
					}
				} while (replay == true);
				Console.WriteLine("\nAlright then, what game would you like to play?\n");
			} else {
				Console.WriteLine("Sorry, that command does not exist.\n\n");
			}
			
		}
	}
	public static string Outcome(string rps, string randmove, string[] moves) {
		string result;
		int plr = Array.IndexOf(moves, rps);
		int com = Array.IndexOf(moves, randmove);
		if (plr - com == 1 || plr - com == -2) {
			result = rps + " beats " + randmove + ", you win!";
		} else if ( plr - com == -1 || plr - com == 2) {
			result = randmove + " beats " + rps + ", you lose!";
		} else {
		 result = "You both chose " + rps + ", so it's a draw!";
		}
		return result;
	}
	public static bool Nacboard() {
		bool ginval = false;
		bool coord = false;
		string lastturn = "O";
		string input;
		string[] rowletters = {"a", "b", "c"};
		int row = 0;
		int col = 0;
		bool end = false;
		bool menu = false;
		string winner = " ";
		int drawcount;
		string[,] board = {{" ", " ", " "}, {" ", " ", " "}, {" ", " ", " "}};
		Console.WriteLine("\nStarting Naughts and crosses...\n");
		while (end == false) {
			do {
				drawcount = 9;
				for (int r = 0; r<3; r++) {
					for (int c = 0; c<3; c++) {
						if (c == 0) {
							Console.Write(rowletters[r]);
						}
						Console.Write(" " + board[r,c] + " ");
						if (c<2) {
							Console.Write("|");
						}
					}
					Console.WriteLine();
					if (r<2) {
						Console.WriteLine(" -----------");
					}
					if (r == 2) {
						Console.WriteLine("  1   2   3 ");
					}
				}
				for (int p = 0; p < 3; p++) {
					if (board[p,0] == board[p,1] && board[p,0] == board[p,2] && board[p,0] != " " || board[0,p] == board[1,p] && board[0,p] == board[2,p] && board[0,p] != " ") {
						winner = lastturn;
					} else if (board[p,0] != "_" && board[p,1] != " " && board[p,2] != " ") {
						drawcount = drawcount-3;
					}
				}
				if (winner != " ") {
					Console.WriteLine(winner + " wins!");
					end = true;
				} else if (board[0,0] == board[1,1] && board[0,0] == board[2,2] && board[0,0] != " " || board[0,2] == board[1,1] && board[0,2] == board[2,0] && board[0,2] != " ") {
					Console.WriteLine(lastturn + " wins!");
					end = true;
				} else if (drawcount == 0) {
					Console.WriteLine("It's a draw!");
					end = true;
				} else {
					if (ginval == false) {
						if (lastturn == "O") {
							lastturn = "X";
						} else {
							lastturn = "O";
						}
					}
					ginval = false;
					Console.WriteLine (lastturn + "'s turn, please input the coordinate you want to play in (e.g. a1, b3), or type 'menu' to exit.");
					input = Console.ReadLine();
					if (input.Length >1) {
						row = Array.IndexOf(rowletters, input.Remove(1));
						coord = int.TryParse(input.Remove(0,1), out col);
						col--;
						if (col < 0 || col >2) {
							coord = false;
						}
					} else {
						coord = false;
					}
					if (coord == false || row == -1) {
						if (input == "menu") {
							Console.WriteLine("Are you sure? (type yes to exit)");
							input = Console.ReadLine();
							if (input.ToLower() == "yes") {
								end = true;
								menu = true;
							} else {
								ginval = true;
							}
						} else{
							Console.WriteLine("\nInvalid coordinate\n");
							ginval = true;
						}
					} else if (board[row,col] == " ") {
						board[row,col] = lastturn;
						Console.WriteLine();
					} else {
						Console.WriteLine("\nInvalid coordinate\n");
						ginval = true;
					}
				}
			} while (ginval == true);
		}
		return menu;
	}
	public static bool Mind(Random rand) {
		string[] colours = {"!", "@", "#", "$", "%", "^"};
		int correct = 0;
		string[] code = {colours[rand.Next(0,5)], colours[rand.Next(0,5)], colours[rand.Next(0,5)], colours[rand.Next(0,5)]};
		string input;
		string inpart;
		bool menu = false;
		bool end = false;
		bool ginval = false;
		int guesses = 5;
		Console.WriteLine("Welcome to Mastermind! Four symbols selected from six (!, @, #, $, %, and ^) have been placed in a sequence, and you have to figure out what that sequence is.");
		Console.WriteLine("\nSymbols can be in the sequence multiple times. After each guess, another sequence of 4 symbols appears below, giving you extra information about the correct sequence.");
		Console.WriteLine("\nIf the symbol is '_', the character in that spot of your guess does not appear in the sequence. If it is a '?', the character in that spot of the guess does appear in the sequence at least once, just in a different place.");
		Console.WriteLine("\nFinally, if the symbol is '*', the character in that spot of your guess also appears in that spot of the sequence. You have 5 guesses to get the correct sequence. Have fun!");
		Console.WriteLine("\n(Also, you can type 'menu' at any time to exit the game.)");
		while(end == false) {
			ginval = false;
			correct = 0;
			if (guesses > 1) {
				Console.WriteLine("\nYou have " + guesses + " guesses left.");
			} else {
				Console.WriteLine("\nYou have " + guesses + " guess left");
			}
			input = Console.ReadLine();
			if (input.Length == 4) {
				if (input == "menu") {
					end = true;
					menu = true;
				} else {
					for (int c = 0; c<4; c++) {
						inpart = input.Remove(0,c);
						if (Array.IndexOf(colours, inpart.Remove(1,3-c)) == -1) {
							ginval = true;
						}
					}
					if (ginval == false) {
						for (int c = 0; c<4; c++) {
							inpart = input.Remove(0,c);
							if (code[c] == inpart.Remove(1,3-c)) {
								Console.Write("*");
								correct++;
							} else if (Array.IndexOf(code, inpart.Remove(1,3-c)) != -1) {
								Console.Write("?");
							} else {
								Console.Write("_");
							}
						}
						guesses--;
					} else {
						Console.WriteLine("Invalid guess, try again.");
					}
					if (correct == 4) {
						Console.WriteLine("\nYou guessed the code, you win!");
						end = true;
					} else if (guesses == 0) {
						Console.Write("You couldn't guess the code in 10 guesses, you Lose!\nThe code was ");
						foreach(string part in code) {
							Console.Write(part);
						}
						Console.WriteLine();
						end = true;
					}
				}
			} else {
				Console.WriteLine("Invalid guess, try again.");
			}
		}
		return menu;
	}
}
