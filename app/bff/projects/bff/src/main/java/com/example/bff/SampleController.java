package com.example.bff;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import mainBusiness.MainBusinessGrpc;
import mainBusiness.MainBusinessOuterClass;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

/**
 * RESTエンドポイント
 */
@RestController
public class SampleController {

    /**
     * ログイン
     * @param name ユーザ名
     * @param password パスワード
     * @return ログイン可否
     */
    @GetMapping("/grpc/{name}/{password}")
    public String hello(@PathVariable String name, @PathVariable String password) {
        ManagedChannel channel = ManagedChannelBuilder
                                    .forAddress("localhost", 5240)
                                    .usePlaintext()
                                    .build();

        MainBusinessGrpc.MainBusinessBlockingStub stub = MainBusinessGrpc.newBlockingStub(channel);

        MainBusinessOuterClass.LoginRequest request = MainBusinessOuterClass.LoginRequest.newBuilder()
                .setName(name)
                .setPassword(password)
                .build();

        MainBusinessOuterClass.LoginReply reply = stub.login(request);

        System.out.println("Reply: " + reply);

        return reply.getMessage();
    }
}
