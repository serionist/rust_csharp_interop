# Introduction
This mini project is used to:
1. Produce an Iterator of a 2D array in Rust
2. Consume the Iterator values in C#

Limitations: C# Unmarshals the returned Iterator value as a flat (1D array) instead of a 2D array that is provided by Rust. We honestly don't care.

# Prerequisites

Have Rust and .NET 6 installed.

# Run

Run `run.sh`
It will:
1. Build the Rust library
2. Copy it to the dotnet folder
3. Run the dotnet app
4. It will call the Rust library to produce the iterator, read the values then print it to the console.
5. Profit???