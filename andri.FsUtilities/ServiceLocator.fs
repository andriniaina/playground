namespace andri.Utilities

module ServiceLocator =
    let private objects = new System.Collections.Generic.Dictionary<obj,obj>()
    let clear() = objects.Clear()

    let get key (f:unit->'r) : 'r =
        lock objects (fun()->
            if objects.ContainsKey(key) then (objects.Item(key) :?> 'r) else f()
        )
    
    let forceSet key v =
        lock objects (fun()->
            if objects.ContainsKey(key) then
                objects.[key] <- v
            else
                objects.Add(key, v)
            )