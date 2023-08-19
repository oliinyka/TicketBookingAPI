# TicketBookingAPI

How to test an API:
1. Create a movie:
Post
api/Movie/movies

{
  "id": 1,
  "title": "111",
  "description": "111",
  "genre": "111"
}

2. Create a theatre with seating arrangement(each row can have different number of seats):
Post
api/Theater/theaters

{
  "id": 1,
  "name": "string",
  "seatingArrangement": [
    {
      "id": 1,
      "rowNumber": 1,
      "numberOfSeats": 3
    }
  ],
  "showtimes": null
}

4. Create a showtime:
Post
api/Showtime/add

{
  "theaterId": 1,
  "movieId": 1,
  "showDateTime": "2023-08-19T12:09:24.843Z"
}

5. Add reservation for certain showtime and seats to reserve
api/Reservation/create

{
  "showtimeId": 1,
  "seats": [
    {
      "rowNumber": 1,
      "seatNumber": 1
    }
  ]
}

TODO:
1. Review and change all input objects to requests (should contain only needed data)
2. Add tests
3. Add booking confirmation endpoint
4. Think about reservation timeout. Probably shoud be impelented a separate service/job
