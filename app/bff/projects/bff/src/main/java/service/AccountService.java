package service;

import config.Config;
import interfaces.IAccountService;
import mainBusiness.MainBusinessGrpc;
import mainBusiness.MainBusinessOuterClass;
import org.springframework.stereotype.Service;

/**
 * アカウントサービス
 */
@Service
public class AccountService implements IAccountService {
    /**
     * ログイン
     * @param name ユーザ名
     * @param password パスワード
     * @return ログイン可否
     */
    @Override
    public MainBusinessOuterClass.LoginReply login(String name, String password) {
        MainBusinessGrpc.MainBusinessBlockingStub stub = MainBusinessGrpc.newBlockingStub(Config.channel);
        MainBusinessOuterClass.LoginRequest request = MainBusinessOuterClass.LoginRequest.newBuilder()
                .setName(name)
                .setPassword(password)
                .build();

        MainBusinessOuterClass.LoginReply reply = stub.login(request);
        return reply;
    }
}
