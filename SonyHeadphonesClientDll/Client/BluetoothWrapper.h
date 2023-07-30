#pragma once

#include "IBluetoothConnector.h"
#include "CommandSerializer.h"
#include "Constants.h"
#include <memory>
#include <vector>
#include <string>
#include <mutex>


//Thread-safety: This class is thread-safe.
class BluetoothWrapper
{
public:
	BluetoothWrapper(std::unique_ptr<IBluetoothConnector> connector);

	BluetoothWrapper(const BluetoothWrapper&) = delete;
	BluetoothWrapper& operator=(const BluetoothWrapper&) = delete;

	BluetoothWrapper(BluetoothWrapper&& other) noexcept;
	BluetoothWrapper& operator=(BluetoothWrapper&& other) noexcept;

	int sendCommand(const std::vector<char>& bytes);

	bool isConnected() noexcept;
	//Try to connect to the headphones
	bool connect(const std::string& addr);
	void real_disconnect() noexcept;
	void disconnect() noexcept;
	bool refresh() noexcept;

	std::vector<BluetoothDevice> getConnectedDevices();

private:
	void _waitForAck();

	std::string _addr;
	std::unique_ptr<IBluetoothConnector> _connector;
	std::mutex _connectorMtx;
	unsigned int _seqNumber = 0;
};