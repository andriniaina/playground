package main

import (
	"fmt"
	"net/http"
	"os"
)

// Define pair as a struct with two fields, ints named x and y.
type pair struct {
	x, y int
}

// Stringer as an interface type with one method, String.
type Stringer interface {
	String() string
}

// Define a method on type pair. Pair now implements Stringer because Pair has defined all the methods in the interface.
func (p pair) String() string { // p is called the "receiver"
	// Sprintf is another public function in package fmt.
	// Dot syntax references fields of p.
	return fmt.Sprintf("(%d, %d)", p.x, p.y)
}

// c is a channel, a concurrency-safe communication object.
func inc(i int, c chan int) {
	c <- i + 1 // <- is the "send" operator when a channel appears on the left.
}

// A single function from package http starts a web server.
func learnWebProgramming() {

	// First parameter of ListenAndServe is TCP address to listen to.
	// Second parameter is an interface, specifically http.Handler.
	go func() {
		err := http.ListenAndServe(":8080", pair{})
		fmt.Println(err) // don't ignore errors
	}()

}

// Make pair an http.Handler by implementing its only method, ServeHTTP.
func (p pair) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	// Serve data with a method of http.ResponseWriter.
	w.Write([]byte("You learned Go in Y minutes!"))
}

func main() {
	var p = pair{3, 4}
	var IString Stringer = p
	fmt.Println(IString.String())

	var a = [...]int{10, 11, 12, 13}
	var s1 = []int{1, 2, 3}
	var s2 = []int{4, 5, 6}
	var s3 = append(s1, s2...)
	m := map[string]int{"three": 3, "four": 4}

	fmt.Println("Goodbye, world!", s1, s2, s3, m)
	if learnDefer() {
		for i := 0; i < len(a); i++ {
			fmt.Println(a[i])
		}
	}

	c := make(chan int)
	go inc(0, c) // go is a statement that starts a new goroutine.
	go inc(10, c)
	go inc(-805, c)
	// Read three results from the channel and print them out.
	// There is no telling in what order the results will arrive!
	fmt.Println(<-c, <-c, <-c) // channel on right, <- is "receive" operator.

	learnWebProgramming()

	fmt.Println("Press any key...")
	var b = make([]byte, 1)
	os.Stdin.Read(b)
}
func learnDefer() (ok bool) {
	// A defer statement pushes a function call onto a list. The list of saved
	// calls is executed AFTER the surrounding function returns.
	defer fmt.Println("deferred statements execute in reverse (LIFO) order.")
	defer fmt.Println("\nThis line is being printed first because")
	// Defer is commonly used to close a file, so the function closing the
	// file stays close to the function opening the file.
	return true
}
