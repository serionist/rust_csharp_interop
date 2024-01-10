#/bin/bash

# build the Rust app
(cd rust_app;cargo build --release)
# Copy the created Rust binary to the dotnet folder
cp rust_app/target/release/librust_test.dylib dotnet/rust_test.dylib
# Run the dotnet app
(cd dotnet; dotnet run)