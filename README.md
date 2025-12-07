# NotEnoughBooks
---
A web app to track what books you already own, so you don't buy the same book twice.

## Usage:
The first user, that is created after the first start, becomes the Admin user. The Admin user can disable further registrations to the instance using the Configuration page.<br>
To use your phone camera to scan the ISBN numbers on your books you can use [BinaryEye](https://github.com/markusfisch/BinaryEye). After installing the app should automatically open when pressing the "Open BinaryEye" button on the add book page. After scanning the ISBN, the website should reopen with the correct ISBN filled in.
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
