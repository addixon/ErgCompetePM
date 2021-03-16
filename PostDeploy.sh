sudo systemctl stop Erg
sudo rm -f /etc/systemd/system/Erg.service
sudo cp Erg.service /etc/systemd/system/Erg.service
sudo chmod +x ErgCompetePM;
sudo systemctl daemon-reload
sudo systemctl enable Erg