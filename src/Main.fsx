module PredictiveText.Main

#r "../node_modules/fable-core/Fable.Core.dll"
#load "../node_modules/fable-import-fetch/Fable.Import.Fetch.fs"
#load "../node_modules/fable-import-fetch/Fable.Helpers.Fetch.fs"
#load "Ui.fsx"

open System
open Fable.Core
open Fable.Import.Fetch
open Fable.Helpers.Fetch
open PredictiveText.Ui
open PredictiveText.Core

Fable.Import.Node.require.Invoke("core-js") |> ignore

async {
    try
        // download list of 20,000 most common English words and insert into t9
        let! fetched = fetchAsync("https://raw.githubusercontent.com/first20hours/google-10000-english/master/20k.txt", [])
        let! txt = fetched.text() |> Async.AwaitPromise

        let model =
            { Tree = (Trie.emptyNode, txt.Split '\n') ||> Array.fold Trie.T9.insert
              Code = "" }
        
        let view = View(model)
        view.Init()
    with
    | error -> Console.WriteLine error
} |> Async.Start