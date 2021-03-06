//---
//ComponentName: Rp
//camelCaseComponentName: rp
//ComponentChar: r
//ComponentNamespace: dash_html_components
//ComponentType: Rp
//LibraryNamespace: Dash.NET.HTML
//---

namespace Dash.NET.HTML

open Dash.NET
open System
open Plotly.NET
open HTMLPropTypes

[<RequireQualifiedAccess>]
module Rp =

    type Rp() =
        inherit DashComponent()
        static member applyMembers
            (
                children : seq<DashComponent>,
                ?Id : string,
                ?ClassName : string,
                ?Style : DashComponentStyle
            ) =
            (
                fun (r:Rp) -> 

                    let props = DashComponentProps()

                    children 
                    |> DashComponent.transformChildren
                    |> DynObj.setValue props "children"

                    Id |> DynObj.setValueOpt props "id"
                    ClassName |> DynObj.setValueOpt props "className"
                    Style |> DynObj.setValueOpt props "style"

                    DynObj.setValue r "namespace" "dash_html_components"
                    DynObj.setValue r "props" props
                    DynObj.setValue r "type" "Rp"

                    r

            )
        static member init 
            (
                children,
                ?Id,
                ?ClassName,
                ?Style
            ) = 
                Rp()
                |> Rp.applyMembers 
                    (
                        children,
                        ?Id = Id,
                        ?ClassName = ClassName,
                        ?Style = Style
                    )

    let rp (props:seq<HTMLProps>) (children:seq<DashComponent>) =
        let r = Rp.init(children)
        let componentProps = 
            match (r.TryGetTypedValue<DashComponentProps>("props")) with
            | Some p -> p
            | None -> DashComponentProps()
        props
        |> Seq.iter (fun prop ->
            let fieldName,boxedProp = prop |> HTMLProps.toDynamicMemberDef
            boxedProp |> DynObj.setValue componentProps fieldName
        )
        componentProps |> DynObj.setValue r "props" 
        r :> DashComponent