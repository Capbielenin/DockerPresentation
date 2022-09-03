Welcome to Docker presentation files for Capgemini .NET community!
Instruction for running:
1. Install Docker on Windows via WSL (https://betterprogramming.pub/how-to-install-docker-without-docker-desktop-on-windows-a2bbb65638a1)
   Remeber: firstly ensure of WSL 2 usage, mixing WSL 1 and WSL 2 can cause problems with docker installation.
2. In case of problem with VPN, use apps like vpnkit (end of article: https://medium.com/twodigits/install-docker-on-wsl-2-with-vpn-support-to-replace-docker-for-windows-45b8e200e171)
3. Open wsl (type wsl in powershell) and firstly create three docker volumes, before running docker-compose up: sqluser, sqlsystem and postgres. Command: docker volume create <volume name>
4. Run docker-compose up in the same folder as docker-compose file

In case of problems or questions message me via Teams at Bielenin, Lukasz.
Have fun!