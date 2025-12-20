# NotEnoughBooks
---
A web app to track what books you already own, so you don't buy the same book twice.

## Usage:
The first user, that is created after the first start, becomes the Admin user. The Admin user can disable further registrations to the instance using the Configuration page.<br>
### How to add Books
To use your phone camera to scan the ISBN numbers on your books you can use [BinaryEye](https://github.com/markusfisch/BinaryEye).

The app can then be opened by clicking the open BinaryEye button on the add book page, after scanning something it should return to that site with the isbn filled in.

You can also setup BinaryEye to open the create book page directly. Just setup the forwarding feature in the settings with this url `<domain>/Book/CreateBook?query=`. As request type set `open in external Browser` and you are good to go
## Setup:
Use docker compose. You can find the image on docker hub [here](https://hub.docker.com/r/bluecatme/notenoughbooks).<br>
You can also use this example docker compose file.
```yaml
services:
  notenoughbooks:
    image: bluecatme/notenoughbooks
    restart: unless-stopped
    ports:
      - "5000:8080"
    volumes:
      - NotEnoughBooks:/app/AppData
volumes:
  NotEnoughBooks:
```
