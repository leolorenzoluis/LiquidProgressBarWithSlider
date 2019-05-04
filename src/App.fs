module App.View

open Elmish
open Fable.React
open Fable.React.Props
open Fulma
open Fable.FontAwesome
open System

type Model =
    { Value : string }

type Msg =
    | ChangeValue of string

let init _ = { Value = "0" }, Cmd.none

let private update msg model =
    match msg with
    | ChangeValue newValue ->
        { model with Value = newValue }, Cmd.none

let classNames =
    List.choose (fun (txt,add) -> if add then Some txt else None)
    >> String.concat " "

let progressBarClassName model =
  let barValue = sprintf "bar-%A" model.Value
  ["bar"; "navy"; "lightGray-face"; barValue] |> String.concat " "

let progressBar model =
  div [ ClassName "chart grid"]
    [
      div [ ClassName "exercise second"] [
        div [ ClassName (progressBarClassName model) ] [
          div [ ClassName "face top" ] [
            div [ ClassName "growing-bar" ] []
          ]
          div [ ClassName "face side-0" ] [
            div [ ClassName "growing-bar" ] []
          ]
          div [ ClassName "face floor" ] [
            div [ ClassName "growing-bar" ] []
          ]
          div [ ClassName "face side-a" ] []
          div [ ClassName "face side-b" ] []
          div [ ClassName "face side-1" ] [
            div [ ClassName "growing-bar" ] []
          ]
        ]
      ]
    ]
// rangeSlider.value;
//   var bulletPosition = (rangeSlider.value /rangeSlider.max);
//   rangeBullet.style.left = (bulletPosition * 578) + "px";

let computeBulletLeft model =
  Fable.Core.JS.console.log(model.Value)
  let bulletPosition = (float model.Value / 100.) * 578.
  Fable.Core.JS.console.log(bulletPosition)
  let left = sprintf "%Apx" bulletPosition
  [
    yield Left left
  ]

let slider model dispatch =
  div [ ClassName "slider-container" ]
    [
      div [ ClassName "range-slider" ]
          [
            span [  Style (computeBulletLeft model);Id "rs-bullet"; ClassName "rs-label"; ] [ str model.Value]
            input [ Id "rs-range-line"; ClassName "rs-range"; Fable.React.Props.HTMLAttr.Type "range";
                    Value model.Value; Min 0; Max 100;
                    OnChange (fun ev -> ev.Value |> ChangeValue |> dispatch) ]
          ]
      div [ ClassName "box-minmax" ]
        [
          span [] [ str "0"]
          span [] [ str "100"]
        ]
    ]

let test model dispatch =
  // div [ ClassName "slider-container" ]
  //   [
      // div [ ClassName "range-slider" ]
      //     [
        //  span [ Style [ Height "500px"]][]
            span [ Style [Height "300px"]][]
            // input [ Id "rs-range-line"; ClassName "rs-range"; Fable.React.Props.HTMLAttr.Type "range";
            //          Min 0; Max 100;
            //         OnChange (fun ev -> ev.Value |> ChangeValue |> dispatch) ]
          // ]
      // div [ ClassName "box-minmax" ]
      //   [
      //     span [] [ str "0"]
      //     span [] [ str "100"]
      //   ]
    // ]

let private view model dispatch =
    Hero.hero [ Hero.IsFullHeight ]
        [ Hero.body [ ]
            [ Container.container [ ]
                [ Columns.columns [ Columns.CustomClass "has-text-centered" ]
                    [ Column.column [ Column.Width(Screen.All, Column.IsOneThird)
                                      Column.Offset(Screen.All, Column.IsOneThird) ]
                        [ Content.content [ ]
                            [
                              progressBar model
                              slider model dispatch
                            ] ] ] ] ] ]
    // span [ Style [ Height "500px"]][]
    // slider model dispatch
    // test model dispatch

open Elmish.Debug
open Elmish.HMR

Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
