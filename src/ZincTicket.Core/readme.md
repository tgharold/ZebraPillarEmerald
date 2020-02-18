## SQLite issues

### In-Memory Databases

In-memory databases only exist for as long as there is a single connection.

### DateTimeOffset / DateTime

- https://www.sqlite.org/datatype3.html#date_and_time_datatype
- https://www.sqlite.org/quirks.html#no_separate_datetime_datatype

Need to make sure that date/time values are always in kind UTC if using DateTime.
